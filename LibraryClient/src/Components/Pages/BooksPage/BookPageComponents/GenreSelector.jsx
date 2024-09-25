import { useCallback, useEffect, useState } from "react"
import { GenreServcie } from "../../../../Services/GenreService"

export default function GenreSelector({setValue}){
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
        selectItems = genres.map(genre => <option key={genre.id} value={genre.normalizedName}>
                                            {genre.name}
                                          </option>)
    }

    return(
        <>
        <label htmlFor="genreSelector" className="mb-2 ml-2">Genres</label>
        <select 
            className="form-select ml-2" 
            name="genreSelector"
            onChange={(e) => setValue(e.target.value)}
        >
            <option selected value={""}>All</option>
            {selectItems}
        </select>
        </>
    )
}