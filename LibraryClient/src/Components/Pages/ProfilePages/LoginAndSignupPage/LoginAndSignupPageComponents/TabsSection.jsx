import ButtonPrimary from "../../../../Common/ButtonPrimary"

export default function TabsSection({active, onChage}){
    return(
        <ul className="nav mb-2">
            <li className="nav-item">
                <ButtonPrimary className="mx-1" isActive={active==='login'} onClick={() => onChage('login')}>LogIn</ButtonPrimary>
            </li>
            <li className="nav-item">
                <ButtonPrimary className="mx-1" isActive={active==='signup'} onClick={() => onChage('signup')}>SignUp</ButtonPrimary>
            </li>
        </ul>
    )
}