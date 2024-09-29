import { useCallback, useEffect, useState } from "react"
import { AuthorService } from "../../../../../Services/AuthorService"

export default function AuthorSelector({onChange, authorId}){
    const [authors, setAuthors] = useState([])
    const [loading, setLoading] = useState(false)
    let selectItems = null

    const fetchAuthors = useCallback(async () => {
        setLoading(true)
        const data = await AuthorService.getAuthors()
        setAuthors(data)
        setLoading(false)
    }, [])

    useEffect(()=>{
        fetchAuthors()
    }, [fetchAuthors])

    if (loading){
        selectItems = null
    }
    else{
        selectItems = authors.map(author => <option key={author.id} value={author.id} selected={author.id === authorId}>   
                                                {`${author.name} ${author.surname}`}
                                            </option>)
    }

    return(
        <>
            <label htmlFor="authorSelector" className="mb-2 ml-2">Author:</label>
            <select 
                className="form-select ml-2" 
                name="authorSelector"
                onChange={(e) => {
                    onChange(e.target.value)
                }}
            >
                {selectItems}
            </select>
        </>
    )
}