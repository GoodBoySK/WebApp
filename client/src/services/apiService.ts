import axios, { type AxiosRequestConfig } from "axios";
import {IsTokenValid} from "@/services/authenticationService";
import router from "@/router";

export const apiUrl = "http://localhost:8080/api/";

const client = axios.create({
    baseURL: apiUrl,
    headers: {
        'Content-Type': 'application/json'
    }    
})


client.interceptors.request.use(
    async (config) => {
        let token = sessionStorage.getItem('jwt');

        if (!token) return config;

        if (IsTokenValid(token)) {
            config.headers.Authorization = `Bearer ${token}`;
            config.withCredentials = true;
        }
        else {
            sessionStorage.removeItem('jwt');
        }

        return config;
    },
    (error) => {
        return Promise.reject(error);
    }
);

client.interceptors.response.use(
    (response) => response,
    (error) => {
        if (error.response && error.response.status === 401) {
            router.push("/login");
        }
        console.log(error.response.status);
        if (error.response && error.response.status === 404) {
            router.push("/notFound");
        }
        return Promise.reject(error);
    }
)

export default {
    async post<RecType>(url: string, data: any, config: AxiosRequestConfig | undefined = undefined)
    {
        if (config)
            return await client.post<RecType>(url, data, config);

        return await client.post<RecType>(url, data);
    },
    async get<RetType>(url: string) 
    {
        return await client.get<RetType>(url);
    },
    async put(url: string, data: any)
    {
        return await client.put(url, data);
    },
    async delete(url: string)
    {
        return await client.delete(url);
    }

}

