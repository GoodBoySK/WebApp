import apiService, { type ApiError } from "./apiService";
import { jwtDecode } from "jwt-decode";

export interface UserData{
    id: string,
    userName: string
    email: string
}

interface TokenAnswear
{
    email: string;
    logInToken: string;
    refreshToken: string;
} 

interface ResetToken
{
    oldPassword: string;
    newPassword: string;
    token: string;
    email: string;
}

export interface UpdateUserInfo {
    newName: string;
}

export function isLogged() : boolean {
    return sessionStorage.getItem('jwt') != null;
}

export async function resetPassword(oldPassword:string, newPassword:string, token:string, email:string) {
    let param: ResetToken = {
        newPassword: newPassword,
        oldPassword: oldPassword,
        token: token,
        email: email
    };

    let answear = await apiService.post("account/resetPassword", param);

    return answear[1];
}

export async function ResetPasswordRequest(email:string) {
    let answear = await apiService.post("account/requestResetPassword", email);

    return answear[1];
}

export async function updateUserInfo(newUserInfo: UpdateUserInfo) {
    let answear = await apiService.put("account", newUserInfo);

    return answear;
}

export async function getLoggedUserInfo() : Promise<UserData | undefined> {
    let user = await apiService.get<UserData>("account/loggedUser")

    if (user[1]) {
        console.error("User is not logged\n" + user[1]);
        return undefined;
    }
    return user[0];
}

// Return errors
export async function register(email: string, name: string, password: string) : Promise<any | ApiError>{
    var response = await apiService.post("account/register", {
        userName: name,
        password: password,
        email: email
    });

    return response[1];   
}

export async function unregister() {
    var error = await apiService.delete("account");

    return error;
}

//return errors
export async function logIn(email: string, password: string ) : Promise<any | ApiError> {
        let response = await apiService.post<TokenAnswear>("account/login", {
            email: email,
            password: password
        });

        if (response[0]){
            let tokens = response[0];

            if (tokens && tokens.logInToken)
                sessionStorage.setItem('jwt', tokens.logInToken);

            return undefined;
        }
        else {
            console.error("User was not logged in !!!\n" + response[1]);
            return response[1];
        }

}

export async function logOut() {
    sessionStorage.removeItem('jwt');
    let response = await apiService.post("account/logout", {});

    if (!response[1]) {
        logoutCallback.forEach(x => {
            x();
        });
    }

    return response[1];
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


let logoutCallback: (() => void)[] = [];
export function MountOnLogOut(func: () => void) {
    logoutCallback.push(func);
}