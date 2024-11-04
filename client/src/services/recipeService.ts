import apiService from "./apiService";
import type { UserData } from "./authenticationService";

export interface MediaFile{
    Id: number;

}

export interface DishType{
    Id: number;
    Name: string;
    Description: string;
}

export interface MediaFile {
    Id: number;
}

export interface Ingredient{
    Name: string;
    Description: string;
}

export interface Instruction{
    Position: number;
    Description: string;
    Media: MediaFile;
}

export interface Reviews {
    Id: string;
    AllReviews: Review[];
    Rating: number;
}

export interface Review {
    Rating: number;

    Id: string;
    CreatedBy: UserData;
    Text: string;
    CreatedAt: Date;
    Parent: Review; 
}

export interface Comment {
    Id: string;
    CreatedBy: UserData;
    Text: string;
    CreatedAt: Date;
    Parent: Comment; 
}

export interface Tag {
    Name: string;
}

export interface Recipe{
    Id: string;
    Name: string;
    Description: string;

    Difficulty: number;
    Portions: number;
    DishType?: DishType ;
    SpotPicture?: MediaFile;
    Ingredients?: Ingredient[];
    Instructions?: Instruction[];
    
    Author?: UserData;
    Reviews?: Reviews;
    Comments?: Comment[];
    Tags?: Tag[];


}

export async function getRecipeById(id : number)
{

    let response = await apiService.get<Recipe>(`recipe/${id}`, {});

    return response;
}