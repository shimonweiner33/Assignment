import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Post, Posts } from '../models/posts-display.model';
import { HubsService } from './hubs.service';


@Injectable({
  providedIn: 'root'
})
export class PostsService {

  private _postListResponse$ = new BehaviorSubject<Posts>(null);
  public postList$ = this._postListResponse$.asObservable();

  constructor(private http: HttpClient, private hubsService: HubsService) {
    this.GetPostList(null);
  }
  GetPostList(posts: Posts) {

    this._postListResponse$.next(null)
    this.http.get("https://localhost:44353/Assignment/GeAllPosts").subscribe((res: Posts) => {
      this._postListResponse$.next(res)

    }, err => {

    })
  }

  AddPost(post: Post) {
    this.http.post("https://localhost:44353/Assignment/CreateOrUpdatePost", post).subscribe((res: Boolean) => {
      // const list = this._postListResponse$.getValue();
      // list.posts.push(post);
      // this._postListResponse$.next(list)
      console.log("add seccessfuly");
    }, err => {

    })
  }
  UpdatePost(post: Post) {
    this.http.post("https://localhost:44353/Assignment/CreateOrUpdatePost", post).subscribe((res: Boolean) => {
      // const list = this._postListResponse$.getValue();
      // const index = list.posts.indexOf(post);
      // if (index > -1) {
      //   list.posts[index] = post;
      //   this._postListResponse$.next(list)
      // }
    }, err => {

    })
  }
  DeletePost(postId: number) {

    this.http.post("https://localhost:44353/Assignment/DeletePost", postId).subscribe((res: Boolean) => {
      const list = this._postListResponse$.getValue();
      list.posts = list.posts.filter(x => x.id !== postId);
      this._postListResponse$.next(list)
    }, err => {

    })
  }
  // private getData<T>(url: string): Observable<T> {

  //   return this.http.get<T>(url)
  // }
  updatePostListAfterChangesByOther() {
    this.hubsService._hubConnecton.on('CreateOrUpdatePost', post => {
      const list = this._postListResponse$.getValue();
      var index = list.posts.map(function (x) { return x.id; }).indexOf(post.id);
      if (index > -1) {
        list.posts[index] = post;
      }
      else {
        list.posts.push(post);
      }
      this._postListResponse$.next(list)
      // const index = this.postList.indexOf(post);
      // if (index > -1) {
      //   this.postList[index] = post;
      // }
      // else {
      //   this.postList.push(post);
      // }
      console.log(post);
    });
  }
}
