import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Post } from 'src/app/models/posts-display.model';
import { PostsService } from 'src/app/services/post-display.service';

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

  constructor(private postsService: PostsService, private route: ActivatedRoute) {
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

    this.postFormGroup = new FormGroup({
      author: new FormControl(''),
      comment: new FormControl(''),
      id: new FormControl(0),
      image: new FormControl(''),
      title: new FormControl(''),
      isFavorite: new FormControl(false),
    });
  }
  updatePost(post: Post) {
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
