import { apiUrl  } from "./apiService";
import apiService from "./apiService";

export default  function getUrlOfImage(id: string){
    return apiUrl + "images/" + id;
}

export async function saveImg(data: FormData) {

    var response = await apiService.post<string>("images/upload", data, {
        headers: {
            'Content-Type': 'multipart/form-data'
        }
    });

    return response.data;
}