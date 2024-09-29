import { useCallback, useEffect, useState } from "react"
import { GenreServcie } from "../../../../../Services/GenreService"

export default function GenreSelector({onChange, genreId}){
    const [genres, setGenres] = useState([])
    const [loading, setLoading] = useState(false)
    let selectItems = null

    const fetchGenres = useCallback(async () => {
        setLoading(true)
        const data = await GenreServcie.getGenres()
        setGenres(data)
        setLoading(false)
    }, [])

    useEffect(()=>{
        fetchGenres()
    }, [fetchGenres])

    if (loading){
        selectItems = null
    }
    else{
        selectItems = genres.map(genre => <option key={genre.id} value={genre.id} selected={genre.id === genreId}>   
                                            {genre.name}
                                          </option>)
    }

    return(
        <>
            <label htmlFor="genreSelector" className="mb-2 ml-2">Genre:</label>
            <select 
                className="form-select ml-2" 
                name="genreSelector"
                onChange={(e) => {
                    onChange(e.target.value)
                }}
            >
                {selectItems}
            </select>
        </>
    )
}