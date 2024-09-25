import Button from "./Button/Button";

export default function TabsSection({active, onChage}){
    return(
        <section>
            <Button isActive={active==='main'} onClick={() => onChage('main')}>Home</Button>
            <Button isActive={active==='feedback'} onClick={() => onChage('feedback')}>Info</Button>
            <Button isActive={active==='http'} onClick={() => onChage('http')}>Http</Button>
        </section>
    )
}