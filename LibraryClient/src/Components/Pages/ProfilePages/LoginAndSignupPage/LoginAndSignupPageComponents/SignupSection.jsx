import ButtonPrimary from "../../../../Common/ButtonPrimary";

export default function SignupSection(){
    return(
        <form>
            <div className="form-group">
                <label>Name</label>
                <input name="" className="form-control" placeholder="name" type="text"/>   
            </div>
            <div className="form-group">
                <label>Surname</label>
                <input name="" className="form-control" placeholder="surname" type="text"/>   
            </div>
            <div className="form-group">
                <label>Phone</label>
                <input name="" className="form-control" placeholder="phone" type="phone"/>   
            </div>
            <div className="form-group">
                <label>Email</label>
                <input name="" className="form-control" placeholder="email" type="email"/>   
            </div>
            <div className="form-group">
                <label>Password</label>
                <input className="form-control" placeholder="password" type="password"/>
            </div>
            <div className="form-group">
                <ButtonPrimary isActive={true}>Sign Up</ButtonPrimary>
            </div>
        </form>
    )
}