import 'bootstrap/dist/css/bootstrap.min.css'

export default function Button({ children, isActive, ... props }){
    function handleClick(){
        console.log('button clicked')
    }

    const handleMouseEnter = () => console.log('entered')

    let classes = 'btn btn-primary mx-1'
    if (isActive) classes += ' disabled'
    else classes += ' active'
    return <button 
                {... props}
                className={classes}//{isActive ? "btn btn-primary mx-1 disabled" : "btn btn-primary mx-1 active"}
                //onMouseEnter={handleMouseEnter}
                //onDoubleClick={() => console.log('double')}
                >
                    { children }
            </button>
}