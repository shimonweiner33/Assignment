import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { AuthenticationService } from '../../services/authentication.service';
import { Post } from './../../models/posts-display.model';
import { HubsService } from './../../services/hubs.service';
import { PostsService } from './../../services/post-display.service';

@Component({
  selector: 'app-post-list',
  templateUrl: './post-list.component.html',
  styleUrls: ['./post-list.component.css']
})
export class PostListComponent implements OnInit {

  postList: Post[] = [];
  openDialogAdd = false;
  filteredPostList: any = [];
  searchControl: FormControl = new FormControl('');
  postFormGroup: FormGroup;
  fileToUpload: File = null;
  constructor(private postsService: PostsService, private route: ActivatedRoute, private hubsService: HubsService,private authenticationService: AuthenticationService) {
  }
  ngOnInit() {
    this.postsService.postList$.subscribe((posts: any) => {

      this.postList = posts ? posts.posts : [];
      this.filteredPostList = this.postList;
    });

    this.searchControl.valueChanges.subscribe(val => {
      if (!val) {
        this.filteredPostList = this.postList;
      } else {
        this.filteredPostList = this.postList.filter(x => x.title.includes(val));
      }
    });
    this.postsService.updatePostListAfterChangesByOther();
    //this.updatePostListAfterChangesByOther();

    this.initListFormGroup();
  }
  // updatePostListAfterChangesByOther() {
  //   this.hubsService._hubConnecton.on('updatePost', post => {

  //     const index = this.postList.indexOf(post);
  //     if (index > -1) {
  //       this.postList[index] = post;
  //     }
  //     else {
  //       this.postList.push(post);
  //     }
  //     console.log(post);
  //   });
  // }
  initListFormGroup() {
    this.postFormGroup = new FormGroup({
      author: new FormControl(''),
      comment: new FormControl(''),
      id: new FormControl(0),
      image: new FormControl(''),
      title: new FormControl(''),
      isFavorite: new FormControl(false),
      roomNum: new FormControl(1)
    });
  }
  updatePost(post: Post) {
    post.roomNum = (post.roomNum === 0 )? 1 : post.roomNum;
    this.postsService.UpdatePost(post);
  }

  deletePost(postId: number) {
    this.postsService.DeletePost(postId);
  }
  addPost() {
    this.postsService.AddPost(this.postFormGroup.value);
    this.openDialogAdd = false;
    this.postFormGroup.reset();
  }


}
