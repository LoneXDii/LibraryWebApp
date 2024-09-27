import { useState } from "react";
import { AuthenticationService } from "../../../../../Services/AuthenticationService";
import { Navigate } from "react-router-dom";


export default function LoginSection(){
    const [emailError, setEmailError] = useState(false)
    const [passwordError, setPasswordError] = useState(false)

    const [email, setEmail] = useState('')
    const [password, setPassword] = useState('')

    const [isButtonActive, setIsButtonActive] = useState(false)
    const [isRedirrectToProfile, setRedirrectToProfile] = useState(false)

    const [isLoginError, setIsLoginError] = useState(false)

    console.log(AuthenticationService.userId)
    if (AuthenticationService.userId){
        return (<Navigate to="/profile"/>)
    }

    let emailPattern = /^\S+@\S+\.\S+$/
    let passwordPattern = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/

    function handleEmailChange(event){
        let newEmail = event.target.value
        setEmail(newEmail)
        let isOk = emailPattern.test(newEmail)
        setEmailError(!isOk)
        if (isOk && !passwordError){
            setIsButtonActive(true)
        }
    }

    function handlePasswordChange(event){
        let newPass = event.target.value
        setPassword(newPass)
        let isOk = passwordPattern.test(newPass)
        setPasswordError(!isOk)
        if (isOk && !emailError){
            setIsButtonActive(true)
        }
    }

    async function handleButtonClick(){
        try{
            await AuthenticationService.login(email, password)
            setRedirrectToProfile(true)
        }
        catch (error){
            console.log(error.response.status)
            setIsLoginError(true)
        }
    }

    if (isRedirrectToProfile){
        return <Navigate to="/profile" />
    }

    return(
        <>
            {isLoginError && (<div className="alert alert-danger" role="alert">
                Incorrect email or password
            </div>)}
            <label>Email</label>
            <input name="" className="form-control" placeholder="email" type="email"
                   value={email} onChange={handleEmailChange}
                   style={{border: emailError ? '2px solid red' : null}}
            />   
            <label>Password</label>
            <a className="float-end" href="#">Forgot?</a>
            <input className="form-control" placeholder="password" type="password"
                   value={password} onChange={handlePasswordChange}
                   style={{border: passwordError ? '2px solid red' : null}}
            />
            
            <button className="btn btn-primary mt-2" disabled={!isButtonActive}
                    onClick={() => handleButtonClick()}        
            >
               Sign In
            </button>
        </>
    )
}