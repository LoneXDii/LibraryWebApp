import { useState } from "react"

export default function SignupSection(){
    const [nameError, setNameError] = useState(false)
    const [surnameError, setSurnameError] = useState(false)
    const [phoneError, setPhoneError] = useState(false)
    const [emailError, setEmailError] = useState(false)
    const [passwordError, setPasswordError] = useState(false)
    const [repeatPasswordError, setRepeatPasswordError] = useState(false)
    const [isButtonActive, setIsButtonActive] = useState(false)

    let phonePattern = /^[\+][(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$/
    let emailPattern = /^\S+@\S+\.\S+$/
    let passwordPattern = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/

    const [name, setName] = useState('')
    const [surname, setSurname] = useState('')
    const [phone, setPhone] = useState('')
    const [email, setEmail] = useState('')
    const [password, setPassword] = useState('')
    const [repeatPassword, setRepeatPassword] = useState('')

    function handleNameChange(event){
        let newName = event.target.value
        setName(newName)
        let isOk = newName !== ""
        setNameError(!isOk)
        if (isOk && !surnameError && !phoneError && !emailError && !passwordError && !repeatPasswordError){
            setIsButtonActive(true)
        } else {
            setIsButtonActive(false)
        }
    }

    function handleSurnameChange(event){
        let newSurname = event.target.value
        setSurname(newSurname)
        let isOk = newSurname !== ""
        setSurnameError(!isOk)
        if (isOk && !nameError && !phoneError && !emailError && !passwordError && !repeatPasswordError){
            setIsButtonActive(true)
        } else {
            setIsButtonActive(false)
        }
    }

    function handlePhoneChange(event){
        let newPhone = event.target.value
        setPhone(newPhone)
        let isOk = phonePattern.test(newPhone)
        setPhoneError(!isOk)
        if (isOk && !nameError && !surnameError && !emailError && !passwordError && !repeatPasswordError){
            setIsButtonActive(true)
        } else {
            setIsButtonActive(false)
        }
    }

    function handleEmailChange(event){
        let newEmail = event.target.value
        setEmail(newEmail)
        let isOk = emailPattern.test(newEmail)
        setEmailError(!isOk)
        if (isOk && !nameError && !surnameError && !phoneError && !passwordError && !repeatPasswordError){
            setIsButtonActive(true)
        } else {
            setIsButtonActive(false)
        }
    }

    function handlePasswordChange(event){
        let newPass = event.target.value
        setPassword(newPass)
        let isOk = passwordPattern.test(newPass)
        setPasswordError(!isOk)
        if (isOk && !nameError && !surnameError && !phoneError && !emailError && !repeatPasswordError){
            setIsButtonActive(true)
        } else {
            setIsButtonActive(false)
        }
    }

    function handleRepeatPasswordChange(event){
        let newRepPass = event.target.value
        setRepeatPassword(newRepPass)
        let isOk = newRepPass === password
        setRepeatPasswordError(!isOk)
        if (isOk && !nameError && !surnameError && !phoneError && !emailError && !passwordError){
            setIsButtonActive(true)
        } else {
            setIsButtonActive(false)
        }
    }    

    return(
        <form>
            <div className="nav mb-2">
                <div className="form-group nav-item w-50">
                    <label>First name</label>
                    <input name="" className="form-control" placeholder="name" type="text"
                           value={name} onChange={handleNameChange}
                           style={{border: nameError ? '2px solid red' : null}}
                    />   
                </div>
                <div className="form-group nav-item w-50">
                    <label>Last name</label>
                    <input name="" className="form-control" placeholder="surname" type="text"
                           value={surname} onChange={handleSurnameChange}
                           style={{border: surnameError ? '2px solid red' : null}}
                    />   
                </div>
            </div>
            <div className="form-group">
                <label>Phone</label>
                <input name="" className="form-control" placeholder="phone" type="phone"
                       value={phone} onChange={handlePhoneChange}
                       style={{border: phoneError ? '2px solid red' : null}}
                />   
            </div>
            <div className="form-group">
                <label>Email</label>
                <input name="" className="form-control" placeholder="email" type="email"
                       value={email} onChange={handleEmailChange}
                       style={{border: emailError ? '2px solid red' : null}}
                />   
            </div>
            <div className="form-group">
                <label>Password</label>
                <input className="form-control" placeholder="password" type="password"
                       value={password} onChange={handlePasswordChange}
                       style={{border: passwordError ? '2px solid red' : null}}
                />
            </div>
            <div className="form-group">
                <label>Repeat password</label>
                <input className="form-control" placeholder="password" type="password"
                       value={repeatPassword} onChange={handleRepeatPasswordChange}
                       style={{border: repeatPasswordError ? '2px solid red' : null}}
                />
            </div>
            <div className="form-group">
                <button className="btn btn-primary mt-2" disabled={!isButtonActive}     
                >
                Sign Up
                </button>
            </div>
        </form>
    )
}