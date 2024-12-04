import CreateRecipe from "@/views/CreateRecipe.vue";
import apiService from "./apiService";
import type { UserData } from "./authenticationService";


export interface DishType{
    id: number;
    name: string;
    description: string;
}

export interface MediaFile {
    id: string;
    image?: FormData | null;
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
    spotPicture: MediaFile;
    ingredients?: Ingredient[];
    instructions?: Instruction[];
    tags?: Tag[];

    author?: UserData;
    reviews?: Reviews;
    comments?: Comment[];
}

export interface UpdateRecipe {
    name: string;
    description: string;
    time: number;
    difficulty: number;
    portions: number;
    dishTypeId: number;
    spotPicture: MediaFile;
    ingredients?: Ingredient[];
    instructions?: Instruction[];
    tags?: Tag[];
}

export interface CreateRecipe {
    name: string
}

export async function createRecipe(nazov:string) : Promise<Recipe>
{

    let data: CreateRecipe = {name: nazov};
    let response = await apiService.post<Recipe>(`recipe`, data);

    return response.data;
}

export async function getMyRecipes() 
{
    let response = await apiService.post<Recipe[]>(`recipe/all`, {OnlyMy: true});

    return response.data;
}


export async function getRecipeById(id : string) 
{
    let response = await apiService.get<Recipe>(`recipe/${id}`);

    return response.data;
}

export async function saveRecipeById(id : string, updateRecipe: UpdateRecipe )
{
    let response = await apiService.put(`recipe/${id}`, updateRecipe);

    return response.data;
}

export async function deleteRecipeById(id : string)
{
    let response = await apiService.delete(`recipe/${id}`);

    return response.status == 204;
}