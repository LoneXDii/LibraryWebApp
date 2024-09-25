import Button from '../Button/Button';
import { useState } from 'react';
import { bContent } from '../../../TestData/data';

export default function ButtonSection(){
    const [contentType, setContentType] = useState(null)

    let tabContent = null

    function handleClick(type){
        setContentType(type)
    }
    
    if (contentType){
        tabContent = <p>{bContent[contentType]}</p>
    } else {
        tabContent = <p>Press the buton</p>
    }

    return(
        <section>
            <Button 
                onClick={() => handleClick('B1')}
                isActive={contentType === 'B1'}>
                    Button 1
            </Button>
            <Button 
                onClick={() => handleClick('B2')}
                isActive={contentType === 'B2'}>
                    Button 2
            </Button>
            <Button 
                onClick={() => handleClick('B3')}
                isActive={contentType === 'B3'}>
                    Button 3
            </Button>

            {tabContent}
        </section>
    )
}