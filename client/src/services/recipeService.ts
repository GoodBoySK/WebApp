import CreateRecipe from "@/views/CreateRecipe.vue";
import apiService, { type ApiError } from "./apiService";
import type { UserData } from "./authenticationService";
import { effectScope } from "vue";


export interface DishType{
    id: number;
    name: string;
    description: string;
}

export interface MediaFile {
    id: string;
    image?: FormData | null;
    isPresent?: boolean;
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

export interface RecipedFilter {
    nameFilter?: string;
    onlyMy?: boolean;
    pageSize?: number;
    page?: number;
    order?: "name" | "created_at";
    ascending?: boolean;
}

export interface RecipeFilterResponse {
    recipes: Recipe[];
    allCount: number;
}

export async function createRecipe(nazov:string) : Promise<[recipe:Recipe | undefined,errors: any | ApiError]>
{

    let data: CreateRecipe = {name: nazov};
    let response = await apiService.post<Recipe>(`recipe`, data);

    return response;
}

export async function getMyRecipes(page:number, pageSize:number) : Promise<[recipe:RecipeFilterResponse | undefined,errors: any | ApiError]>
{
    let response = await apiService.post<RecipeFilterResponse>(`recipe/all`, {OnlyMy: true, page: page, pageSize: pageSize});

    return response;
}


export async function getRecipeById(id : string) : Promise<[recipe:Recipe | undefined,errors: any | ApiError]>
{
    let response = await apiService.get<Recipe>(`recipe/${id}`);

    return response;
}

export async function saveRecipeById(id : string, updateRecipe: UpdateRecipe ) : Promise<any | ApiError>
{
    let response = await apiService.put(`recipe/${id}`, updateRecipe);

    return response;
}

//Return errors
export async function deleteRecipeById(id : string) : Promise<any | ApiError>
{
    let response = await apiService.delete(`recipe/${id}`);

    return response;
}

export async function getRecipesByFilter(filter?: RecipedFilter) : Promise<[RecipeFilterResponse | undefined, any | ApiError]>{
    let response = await apiService.post<RecipeFilterResponse>('recipe/all', filter);
    
    return response;
}