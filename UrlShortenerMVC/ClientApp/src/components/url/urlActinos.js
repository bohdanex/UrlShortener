import { ErrorResponse } from "../../types/Responses";
import { BaseUrl, TableUrl } from "../../types/UrlTypes";
import { getJWT } from "../../services/storageService";

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

export async function deleteUrl(id: string): Promise<number | ErrorResponse>{
    const jwt = getJWT();
    const url = `api/urlshortener/${id}`;
    const result = await fetch(url,{
        method:"DELETE",
        headers:{
            "Authorization": "Bearer " + jwt
        }
    });
    if(!result.ok){
        return await result.json();
    }
    return result.status;
}

export async function addUrl(url: string): Promise<TableUrl | null>{
    const jwt = getJWT();
    const endpointUrl = 'api/urlshortener/create'
    try{
        const response = await fetch(endpointUrl, {
            method: "POST",
            headers: {
                "Authorization": "Bearer " + jwt,
                "Content-Type": "application/json"
            },
            body: JSON.stringify({url})
        });

        if(!response.ok){
            throw new Error("Bad request")
        }

        return await response.json();
    }
    catch(ex){
        throw ex;
    }
}

export async function getById(id: string): Promise<BaseUrl | ErrorResponse>{
    const url = 'api/urlshortener/get/' + id;
    const jwt = getJWT();
    const response = await fetch(url, {
        method: "GET",
        headers: {
            "Authorization": "Bearer " + jwt,
            "Content-Type": "application/json"
        }
    });
    if(!response.ok){
        const error: ErrorResponse = await response.json();
        return error.errorMessage;
    }

    return await response.json();
}