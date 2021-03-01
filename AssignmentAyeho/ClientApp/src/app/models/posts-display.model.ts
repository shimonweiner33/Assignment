export interface Posts {
    posts: Post[];
}

export interface Post {
    id:        number;
    title:     string;
    author:    string;
    comment:   string;
    image:     string;
    updatedOn: Date;
    createdOn: Date;
    updatedBy: null;
    createdBy: null;
    isFavorite: false;
}