import { apiUrl } from "./apiService";


export default  function getUrlOfImage(id: string){
    return apiUrl + "/images/" + id;
}