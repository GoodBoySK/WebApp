import { reactive } from "vue";
import { setAuthToken } from "./apiService";
import apiService from "./apiService";

export interface UserData{
    userName: string
}

export const authState = reactive({
    isLoggedIn: false,
    jwt: "", 
});

export async function getLoggedUserInfo() {
    try {
        let user = await apiService.get<UserData>("account/loggedUser", {})
        return user;
    } catch (error) {
        console.error("User is not logged");
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
    authState.isLoggedIn = true;

    try {
        let respones = await apiService.post("account/login", {
            email: email,
            password: password
        })

        authState.jwt = respones.token;
    
        setAuthToken(authState.jwt);

        return true;
    } catch (error) {
        
        console.error("User was not logged in (invalid credentials)");
        return false;
    }
   


}
export function logOut() {
    authState.isLoggedIn = false;
    authState.jwt = ""

    setAuthToken("")
}
