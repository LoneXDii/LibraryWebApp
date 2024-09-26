import { useState } from 'react';
import Pagination from 'react-bootstrap/Pagination';

export default function CustomPagination({totalPages, currentPage, changeValue}){

    function handleClick(page){
        if (page <= 0){
            page = 1
        }
        if (page >= totalPages){
            page = totalPages
        }
        changeValue(page)
    }

    let items = []
    for (let i = 1; i <= totalPages; i++){
        items.push(
            <Pagination.Item 
                key={i} 
                active={i === currentPage}
                onClick={() => handleClick(i)}
            >
                {i}
            </Pagination.Item>
        )
    }

    return(
        <>
            <Pagination>
                <Pagination.First onClick={() => handleClick(1)}/>
                <Pagination.Prev onClick={() => handleClick(currentPage - 1)}/>
                {items}
                <Pagination.Next onClick={() => handleClick(currentPage + 1)}/>
                <Pagination.Last onClick={() => handleClick(totalPages)}/>
            </Pagination>
        </>
    )
}