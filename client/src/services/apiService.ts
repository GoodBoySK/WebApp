import axios, { type AxiosResponse } from "axios";
export const apiUrl = "http://localhost:8080/api/";
const client = axios.create({
    baseURL: apiUrl,
    headers: {
        'Content-Type': 'application/json'
    }    
})

export function setAuthToken( token: string)
{
    client.defaults.headers.common['Authorization'] = 'Bearer ' + token;
}
export default {
    async post(url: string, data: any)
    {
        return await client.post(url, data).then((response) => response.data);
    },
    async get<RetType>(url: string, data: any)
    {
        return await client.get<RetType>(url, data).then((response) => response.data);
    },
    async put(url: string, data: any)
    {
        return await client.put(url, data).then((response) => response.data);
    },
    async delete(url: string)
    {
        return await client.delete(url, {}).then((response) => response.data);
    }

}

