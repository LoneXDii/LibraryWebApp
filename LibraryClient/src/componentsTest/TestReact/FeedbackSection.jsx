import 'bootstrap/dist/css/bootstrap.min.css'
import Button from './Button/Button'
import { useRef, useState } from 'react'

function StateVsRef(){
    const input = useRef()
    const [show, setShow] = useState(false)
    
    function handleKeyDown(event){
        if (event.key === 'Enter'){
            setShow(true)
        }
    }

    return(
        <div>
            <h3>Input value: {show && input.current.value}</h3>
            <input 
                ref={input}
                type='text' 
                onKeyDown={handleKeyDown} 
            />
        </div>
    )
}


export default function FeedbackSection(){
    const [form, setForm] = useState({
        name: '',
        hasError: true,
        reason: 'help'
    })

    //const [name, setName] = useState('')
    //const [reason, setReason] = useState('help')
    //const [hasError, setHasError] = useState(true)

    function handleNameChange(event){
        //setName(event.target.value)
        //setHasError(event.target.value.trim().length === 0)
        setForm((prev) => ({
            ...prev, 
            name: event.target.value,
            hasError: event.target.value.trim().length === 0,
        }))
    }

    function toggleError(){
        //setHasError((prev) => !prev)
        //setHasError((prev) => !prev)
        //setHasError(!hasError)
    }

    return(
        <section>
            <h1>New Tab!!!</h1>
            
            <Button onClick={toggleError}>Toggle error</Button>

            <form>
                <label htmlFor="name">Your name</label>
                <input 
                    type='text' 
                    className='form-control' 
                    id='name' 
                    value={form.name} 
                    onChange={handleNameChange}
                    style={{
                        border: form.hasError ? '1px solid red' : null
                    }}
                />

                <label htmlFor="reason">Your reason</label>
                <select 
                    id='reason' 
                    className="form-select" 
                    value={form.reason} 
                    onChange={event => setForm((prev) => ({
                        ...prev, 
                        reason: event.target.value
                    }))}
                >
                    <option value='error'>Error</option>
                    <option value='help'>Help</option>
                    <option value='suggest'>Suggest</option>
                </select>

                <pre>
                    {JSON.stringify(form, null, 2)}
                </pre>
                <Button disabled={form.hasError}>Send</Button>

                <hr/>
                <StateVsRef/>
            </form>
        </section>
    )
}