import axios from "axios";
import {IsTokenValid} from "@/services/authenticationService";
import { useRouter } from "vue-router";

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
        if (error.respones && error.response.status === 401) {
            useRouter().push("/login");
        }
        return Promise.reject(error);
    }
)

export default {
    async post<RecType>(url: string, data: any)
    {
        return await client.post<RecType>(url, data).then((response) => response.data);
    },
    async get<RetType>(url: string)
    {
        return await client.get<RetType>(url).then((response) => response.data);
    },
    async put(url: string, data: any)
    {
        return await client.put(url, data).then((response) => response.data);
    },
    async delete(url: string)
    {
        return await client.delete(url).then((response) => response.data);
    }

}

