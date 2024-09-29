import { useState } from "react"
import { Navigate } from "react-router-dom";
import { AuthenticationService } from "../../../../../Services/AuthenticationService";

export default function SignupSection(){
    const [nameError, setNameError] = useState(false)
    const [surnameError, setSurnameError] = useState(false)
    const [phoneError, setPhoneError] = useState(false)
    const [emailError, setEmailError] = useState(false)
    const [passwordError, setPasswordError] = useState(false)
    const [repeatPasswordError, setRepeatPasswordError] = useState(false)

    let namePattern = /^[a-zA-Z]+$/
    let phonePattern = /^[\+][(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$/
    let emailPattern = /^\S+@\S+\.\S+$/
    let passwordPattern = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/

    const [name, setName] = useState('')
    const [surname, setSurname] = useState('')
    const [phone, setPhone] = useState('')
    const [email, setEmail] = useState('')
    const [password, setPassword] = useState('')
    const [repeatPassword, setRepeatPassword] = useState('')

    const [isRedirrectToProfile, setRedirrectToProfile] = useState(false)

    if (AuthenticationService.userId){
        return (<Navigate to="/profile"/>)
    }

    function handleNameChange(event){
        let newName = event.target.value
        setName(newName)
    }

    function handleSurnameChange(event){
        let newSurname = event.target.value
        setSurname(newSurname)
    }

    function handlePhoneChange(event){
        let newPhone = event.target.value
        setPhone(newPhone)
        let isOk = phonePattern.test(newPhone)
        setPhoneError(!isOk)
    }

    function handleEmailChange(event){
        let newEmail = event.target.value
        setEmail(newEmail)
    }

    function handlePasswordChange(event){
        let newPass = event.target.value
        setPassword(newPass)
        let isOk = passwordPattern.test(newPass)
        setPasswordError(!isOk)
    }

    function handleRepeatPasswordChange(event){
        let newRepPass = event.target.value
        setRepeatPassword(newRepPass)
        let isOk = newRepPass === password
        setRepeatPasswordError(!isOk)
    }    

    async function handleButtonClick(){
        try{
            let isNameOk = namePattern.test(name)
            let isSurnameOk = namePattern.test(surname)
            let isEmailOk = emailPattern.test(email)
            let isPhoneOk = phonePattern.test(phone)
            let isPasswordOk = passwordPattern.test(password)
            let isRepeatPasswordOk = password === repeatPassword

            setNameError(!isNameOk)
            setSurnameError(!isSurnameOk)
            setEmailError(!isEmailOk)
            setPhoneError(!isPhoneOk)
            setPasswordError(!isPasswordOk)
            setRepeatPasswordError(!isRepeatPasswordOk)

            if (!(isNameOk && isSurnameOk && isEmailOk && isPasswordOk && isRepeatPasswordOk && isPhoneOk)){
                return
            }

            await AuthenticationService.register(name, surname, email, password, phone)
            await AuthenticationService.login(email, password)
            setRedirrectToProfile(true)
        } catch(error) {
            console.log(error.response.status)
        }
    }

    if (isRedirrectToProfile){
        return <Navigate to="/profile"/>
    }

    return(
        <>
            <div className="nav mb-2">
                <div className="nav-item w-50">
                    <label>First name</label>
                    <input name="" className="form-control" placeholder="name" type="text"
                            value={name} onChange={handleNameChange}
                            style={{border: nameError ? '2px solid red' : null}}
                    />   
                </div>

                <div className="nav-item w-50">
                    <label>Last name</label>
                    <input name="" className="form-control" placeholder="surname" type="text"
                            value={surname} onChange={handleSurnameChange}
                            style={{border: surnameError ? '2px solid red' : null}}
                    />
                </div>   
            </div>

            <label>Phone</label>
            <input name="" className="form-control" placeholder="phone" type="phone"
                    value={phone} onChange={handlePhoneChange}
                    style={{border: phoneError ? '2px solid red' : null}}
            />   

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
                                    Password must be at least 8 characters long and must contain at least one uppercase and lowercase letters, digid and special character
                                </div>)}
            <input className="form-control" placeholder="password" type="password"
                    value={password} onChange={handlePasswordChange}
                    style={{border: passwordError ? '2px solid red' : null}}
            />

            <label>Repeat password</label>
            <input className="form-control" placeholder="password" type="password"
                    value={repeatPassword} onChange={handleRepeatPasswordChange}
                    style={{border: repeatPasswordError ? '2px solid red' : null}}
            />

            <button className="btn btn-primary mt-2" onClick={() => handleButtonClick()}>
                Sign Up
            </button>
        </>
    )
}