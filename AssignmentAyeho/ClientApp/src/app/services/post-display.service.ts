import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Post, Posts } from '../models/posts-display.model';


@Injectable({
  providedIn: 'root'
})
export class PostsService {

  private _postListResponse$ = new BehaviorSubject<Posts>(null);
  public postList$ = this._postListResponse$.asObservable();

  constructor(private http: HttpClient) {
    this.GetPostList(null);
  }
  GetPostList(posts: Posts) {

    this._postListResponse$.next(null)
    this.http.get("https://localhost:44353/AssignmentAyeho/GeAllPosts").subscribe((res: Posts) => {
      this._postListResponse$.next(res)

    }, err => {

    })
  }

  AddPost(post: Post) {
      this.http.post("https://localhost:44353/AssignmentAyeho/CreateOrUpdatePost", post).subscribe((res: Boolean) => {
      const list = this._postListResponse$.getValue();
      list.posts.push(post);
      this._postListResponse$.next(list)
    }, err => {

    })
  }
  UpdatePost(post: Post) {
    this.http.post("https://localhost:44353/AssignmentAyeho/CreateOrUpdatePost", post).subscribe((res: Boolean) => {
      const list = this._postListResponse$.getValue();
      const index = list.posts.indexOf(post);
      if (index > -1) {
        list.posts[index] = post;
        this._postListResponse$.next(list)
      }
    }, err => {

    })
  }
  DeletePost(postId: number) {

      this.http.post("https://localhost:44353/AssignmentAyeho/DeletePost", postId).subscribe((res: Boolean) => {
      const list = this._postListResponse$.getValue();
      list.posts = list.posts.filter(x => x.id !== postId);
      this._postListResponse$.next(list)
    }, err => {

    })
  }
  // private getData<T>(url: string): Observable<T> {

  //   return this.http.get<T>(url)
  // }

}
