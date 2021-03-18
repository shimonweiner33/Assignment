import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Post } from 'src/app/models/posts-display.model';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.css']
})
export class PostComponent implements OnInit {
@Input() post: Post;
@Output() updatePostEvent = new EventEmitter<Post>();
@Output() deletePostEvent = new EventEmitter<number>();

  postFormGroup: FormGroup;
  constructor() {}

  ngOnInit() {
    this.postFormGroup = new FormGroup({
      author: new FormControl(this.post.author),
      comment: new FormControl(this.post.comment),
      id: new FormControl(this.post.id),
      image: new FormControl(this.post.image),
      title: new FormControl(this.post.title),
      isFavorite: new FormControl(false),
    });
  }

  update() {
    console.log(this.postFormGroup.value);
    this.updatePostEvent.emit(this.postFormGroup.value);
  }
  delete(){
    this.deletePostEvent.emit(this.post.id);
  }

}
