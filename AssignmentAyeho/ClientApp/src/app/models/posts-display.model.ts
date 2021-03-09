export interface Posts {
    posts: Post[];
}

export interface Post {
    id: number;
    userName: string;
    roomNum: number;
    title: string;
    author: string;
    comment: string;
    image: string;
    isFavorite: false;
    updatedOn: Date;
    createdOn: Date;
    updatedBy: null;
    createdBy: null;
}