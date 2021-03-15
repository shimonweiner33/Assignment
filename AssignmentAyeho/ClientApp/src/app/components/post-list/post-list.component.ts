import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Params } from '@angular/router';
import { ConnectedUsers } from '../../models/user.model';
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
  userList: ConnectedUsers[] = [];
  openDialogAdd = false;
  filteredPostList: any = [];
  searchControl: FormControl = new FormControl('');
  postFormGroup: FormGroup;
  fileToUpload: File = null;
  currentRoom: Number;
  constructor(private postsService: PostsService, private route: ActivatedRoute, private hubsService: HubsService, private authenticationService: AuthenticationService) {
  }
  ngOnInit() {
    this.route.params.subscribe((params: Params) => {
      this.currentRoom = parseInt(params['roomNum'])
      this.postsService.GetPostList(this.currentRoom)
    });
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


    this.hubsService.userList$.subscribe((userList: any) => {
      this.userList = userList ? userList : [];
    });

    this.postsService.updatePostListAfterChangesByOther();
    //this.updatePostListAfterChangesByOther();
    this.hubsService.updateUserLogIn();
    this.hubsService.updateUserLogOut();

    this.initListFormGroup();
  }

  initListFormGroup() {
    this.postFormGroup = new FormGroup({
      author: new FormControl(''),
      comment: new FormControl(''),
      id: new FormControl(0),
      image: new FormControl(''),
      title: new FormControl(''),
      isFavorite: new FormControl(false),
      roomNum: new FormControl(this.currentRoom)
    });
  }
  updatePost(post: Post) {
    post.roomNum = (post.roomNum === 0) ? 1 : post.roomNum;
    this.postsService.UpdatePost(post);
  }

  deletePost(postId: number) {
    this.postsService.DeletePost(postId);
  }
  addPost() {
    //this.postFormGroup.value.roomNum = this.currentRoom;
    this.postsService.AddPost(this.postFormGroup.value);
    this.openDialogAdd = false;
    this.postFormGroup.reset();
  }


}
