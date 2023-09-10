import { TableUrl } from "../../types/UrlTypes";

export function getAll(page: number = 0): Promise<Array<TableUrl>>{
    const url = `api/urlshortener/get-all/${page}`;
    return fetch(url,
    {
        method: 'GET',
        headers:{
            "Accept":"application/json"
        }
    })
    .then(response => response.json())
    .catch(error => {throw error;})
}