import { apiService } from "./ApiService";
import ApiConfiguration from "./ApiConfiguration";
import { json } from "react-router-dom";
import { AuthenticationService } from "./AuthenticationService";
import { TokenAcessor } from "./TokenAcessor";

export class BookService{ 
    static async getPaginatedBooks(genre, pageNo = 1, pageSize = 8 ){
        let uri = ApiConfiguration.apiBaseUri + 'api/books/'
        uri += genre === undefined ? '' : genre
        uri += `?pageNo=${pageNo}&pageSize=${pageSize}`
        let response = await apiService.get(uri)
        const responseData = response.data
        return responseData
    }

    static async getBookById(id){
        let uri = ApiConfiguration.apiBaseUri + `api/books/${id}`
        let response = await apiService.get(uri)
        return response.data
    }

    static async updateBook(book, image){
        if(image){
            if(book.image){
                let oldImage = book.image.split('/')
                oldImage = oldImage[oldImage.length - 1]
                let uriDelete = ApiConfiguration.apiBaseUri + `api/files/${oldImage}`
                try{
                    await apiService.delete(uriDelete)
                }
                catch(error){
                    console.log(error)
                }
            }
            const imageGuid = await this.saveImage(image)
            if (imageGuid){
                book.image = ApiConfiguration.apiBaseUri + `api/files/${imageGuid}`
            }
        }

        let updateUri = ApiConfiguration.apiBaseUri + `api/books/${book.id}`
        const response = await apiService.put(updateUri, JSON.stringify(book),{
            headers:{
                'Content-Type': 'application/json',
            }
        })
        
    }

    static async saveImage(image){
        let uriSave = ApiConfiguration.apiBaseUri + 'api/files'
        const formData = new FormData();
        formData.append('file', image);

        try{
            const response = await apiService.post(uriSave, formData, {
                headers:{
                    'Content-Type': 'multipart/form-data',
                }
            })
            console.log('file loaded sucessfully')
            console.log(response.data)
            return response.data
        }
        catch(error){
            console.log('Error in saving file')
        }
    }
}