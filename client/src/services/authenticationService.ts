import apiService from "./apiService";
import { jwtDecode } from "jwt-decode";

export interface UserData{
    id: string,
    userName: string
}

interface TokenAnswear
{
    email: string;
    logInToken: string;
    refreshToken: string;
} 

export function isLogged() : boolean {
    return sessionStorage.getItem('jwt') != null;
}


export async function getLoggedUserInfo() {
    try {
        let user = await apiService.get<UserData>("account/loggedUser")
        return user.data;
    } catch (error) {
        console.error("User is not logged\n" + error);
        return {};
    }
}

export async function register(email: string, name: string, password: string) {
     try {
        await apiService.post("account/register", {
            userName: name,
            password: password,
            email: email
        })
        return true;
    } catch (error) {
        console.error("User was not able to register");
        return false;
    }
   
}

export async function logIn(email: string, password: string ){
    try {
        let response = await apiService.post<TokenAnswear>("account/login", {
            email: email,
            password: password
        });

        let tokens = response.data;

        if (tokens && tokens.logInToken)
            sessionStorage.setItem('jwt', tokens.logInToken);

        return true;
    } catch (error) {
        console.error("User was not logged in !!!\n" + error);
        return false;
    }
}

export function logOut() {
    sessionStorage.removeItem('jwt');

}

 async function refreshAccessToken() {
    ;
}

export function IsTokenValid(jwtToken: string) : boolean {
    if (!jwtToken) return false;
    
    const data = jwtDecode(jwtToken);
    const currentTime = Date.now() / 1000;
    
    if (!data.exp) return true;

    return data.exp > currentTime;
}