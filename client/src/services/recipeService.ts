import apiService from "./apiService";
import type { UserData } from "./authenticationService";

export interface MediaFile{
    id: number;

}

export interface DishType{
    id: number;
    name: string;
    description: string;
}

export interface MediaFile {
    id: number;
}

export interface Ingredient{
    name: string;
    description: string;
}

export interface Instruction{
    position: number;
    description: string;
    media: MediaFile;
}

export interface Reviews {
    id: string;
    allReviews: Review[];
    rating: number;
}

export interface Review {
    rating: number;

    id: string;
    createdBy: UserData;
    text: string;
    createdAt: Date;
    parent: Review; 
}

export interface Comment {
    id: string;
    createdBy: UserData;
    text: string;
    createdAt: Date;
    parent: Comment; 
}

export interface Tag {
    name: string;
}

export interface Recipe{
    id: string;
    name: string;
    description: string;
    time: number;
    difficulty: number;
    portions: number;
    dishType?: DishType ;
    spotPicture?: MediaFile;
    ingredients?: Ingredient[];
    instructions?: Instruction[];
    
    author?: UserData;
    reviews?: Reviews;
    comments?: Comment[];
    tags?: Tag[];


}

export async function getRecipeById(id : number)
{

    let response = await apiService.get<Recipe>(`recipe/${id}`);

    return response;
}