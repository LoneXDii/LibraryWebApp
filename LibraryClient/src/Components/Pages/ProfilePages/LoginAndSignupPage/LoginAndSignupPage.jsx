import { useState } from "react";
import TabsSection from "./LoginAndSignupPageComponents/TabsSection";
import LoginSection from "./LoginAndSignupPageComponents/LoginSection";
import SignupSection from "./LoginAndSignupPageComponents/SignupSection";
import Header from "../../../Common/Header";

export default function LoginAndSingupPage(){
    const [tab, setTab] = useState('login')

    return(
        <>
            <Header/>
            <div className="my-5">
                <div className="mx-auto my-auto w-50 h-50">
                    <div className="card">
                        <div className="card-body">
                            <div className="card-title">
                                <TabsSection active={tab} onChage={(current) => setTab(current)}/>
                            </div>
                            <div className="card-body">
                                {tab === 'login' && <LoginSection/>}
                                {tab === 'signup' && <SignupSection/>}
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </>
    )
}