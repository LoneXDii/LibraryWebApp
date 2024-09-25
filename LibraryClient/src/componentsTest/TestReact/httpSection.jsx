import { useEffect, useState, useCallback } from "react"

export default function HttpSection(){
    const [users, setUsers] = useState([])
    const [loading, setLoading] = useState(false)

    const fetchUsers = useCallback(async ()=>{
        setLoading(true)
        const response = await fetch('https://jsonplaceholder.typicode.com/users')
        const data = await response.json()
        setUsers(data)
        console.log(data)
        setLoading(false)
    }, [])

    useEffect(()=>{
        fetchUsers()
    }, [fetchUsers])

    
    return(
        <>
        {loading && <p>loading...</p>}
        {!loading && 
            <ul>
                {users.map(user => <li key={user.id}>{user.name}</li>)}
            </ul>}
        </>
    )
}