import apiService from "./apiService";
import type { UserData } from "./authenticationService";
import type { Filter, MediaFile, Tag } from "./recipeService";

export interface BlogPost{
    id: number;
    autor: UserData;
    tag: Tag;
    title: string;
    content: string;
    createdAt: Date;
    desctiption: string;
    thumbnail: MediaFile;
}

export interface BlogPostsResponse {
    blogs: BlogPost[];
    count: number;

}

export async function getBlogPost(id:number) {
    const response = await apiService.get<BlogPost>(`/blogpost/${id}`);
    
    return response;
}

export async function getBlogPosts(filter?: Filter) {
    const response = await apiService.post<BlogPostsResponse>(`/blogpost/all`, filter);
    
    return response;
}