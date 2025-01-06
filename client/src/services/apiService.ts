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
    (error) : Promise<ApiError>=> {
        if (!error.response)return Promise.reject<ApiError>({
            type: 'network',
            message: 'Network error. Please check your connection.',
        });

        if (error.response.status === 401) {
            router.push("/login");
            return Promise.reject<ApiError>({
                type: 'unautorized',
                message: 'User is unautorized!!!',
            });
        }
        if (error.response.status === 403) {
            return Promise.reject<ApiError>({
                type: 'forbiden',
                message: 'User is forbiden to do action!!!',
            });
        }
        if (error.response.status === 404) {
            router.push("/notfound");
            return Promise.reject<ApiError>({
                type: 'notFound',
                message: 'Content has not been found!!!',
            })
        }
        if (error.response.status === 400 && error.response.data) {
            return Promise.reject<ApiError>({
                type: 'validation',
                message: error.response.data.message || 'Validation failed',
                errors: error.response.data.errors.map((x: ValidationError) => {
                    x.field = x.field.toLowerCase();
                    return x
                }) || [],
                details: error.response.data.details
            })
        }

        return Promise.reject(error);
    }
)

export function isApiError(error: any): error is ApiError {
    return (
        error && 
        typeof error === 'object' && 
        'type' in error && 
        'message' in error 
    );
}


export interface ValidationError {
    field: string;
    message: string;
}

export interface ApiError {
    type: 'validation' | 'server' | 'network' | 'unautorized' | 'notFound' | 'forbid';
    message: string;
    errors?: ValidationError[];
    details?: string;
}

export default {
    async post<RecType>(url: string, data: any, config: AxiosRequestConfig | undefined = undefined) : Promise<[data:RecType | undefined,error: any | ApiError]>
    {
        if (config)
             return await client.post<RecType>(url, data, config).then(
                data => {
                    return [data.data, undefined];
                },function (error) {
                    return [undefined, error]
                });

        return await client.post<RecType>(url, data).then(
            data => {
                return [data.data, undefined];
            },function (error) {
                return [undefined, error]
            });
    },
    async get<RetType>(url: string) : Promise<[data:RetType | undefined,error: any | ApiError]>
    {
        return await client.get<RetType>(url).then(
            data => {
                return [data.data, undefined];
            },function (error) {
                return [undefined, error]
            });
    },
    async put(url: string, data: any) : Promise<any | ApiError>
    {
        return await client.put(url, data).then(
            () => {
                return undefined;
            },function (error) {
                return error;
            }
        );
    },
    async delete(url: string) : Promise<any | ApiError>
    {
        return await client.delete(url).then(
            () => {
                return undefined;
            },function (error) {
                return error;
            }
        );;
    }

}

