import { useState } from "react";
import { AuthenticationService } from "../../../../../Services/AuthenticationService";
import { Navigate } from "react-router-dom";


export default function LoginSection(){
    const [emailError, setEmailError] = useState(false)
    const [passwordError, setPasswordError] = useState(false)

    const [email, setEmail] = useState('')
    const [password, setPassword] = useState('')

    const [isRedirrectToProfile, setRedirrectToProfile] = useState(false)

    const [isLoginError, setIsLoginError] = useState(false)

    if (AuthenticationService.userId){
        return (<Navigate to="/profile"/>)
    }

    let emailPattern = /^\S+@\S+\.\S+$/

    function handleEmailChange(event){
        let newEmail = event.target.value
        setEmail(newEmail)
    }

    function handlePasswordChange(event){
        let newPass = event.target.value
        setPassword(newPass)
    }

    async function handleButtonClick(){
        try{
            let isEmailOk = emailPattern.test(email)
            let isPasswordOk = !(password === "")
            
            setIsLoginError(false)
            setEmailError(!isEmailOk)
            setPasswordError(!isPasswordOk)

            if(!(isEmailOk && isPasswordOk)){
                return
            }

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
            {emailError && (<div className="alert alert-danger" role="alert">
                                Please, enter a valid email
                            </div>)}
            <input name="" className="form-control" placeholder="email" type="email"
                   value={email} onChange={handleEmailChange}
                   style={{border: emailError ? '2px solid red' : null}}
            />   

            <label>Password</label>
            {passwordError && (<div className="alert alert-danger" role="alert">
                                    Please, enter a password
                                </div>)}
            <a className="float-end" href="#">Forgot?</a>
            <input className="form-control" placeholder="password" type="password"
                   value={password} onChange={handlePasswordChange}
                   style={{border: passwordError ? '2px solid red' : null}}
            />
            
            <button className="btn btn-primary mt-2" onClick={() => handleButtonClick()}>
               Sign In
            </button>
        </>
    )
}