import { TestBed } from '@angular/core/testing';
import { PostsService as PostsService } from './post-display.service';


describe('PostDisplayService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: PostsService = TestBed.get(PostsService);
    expect(service).toBeTruthy();
  });
});
