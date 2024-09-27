export default function ButtonPrimary({ children, isActive, ... props }){
    let classes = 'btn btn-primary mx-1'
    if (isActive) {
        classes += ' disabled'
    } else {
        classes += ' active'
    }
    return (
        <button {... props}  className={classes}>
            { children }
        </button>
    )
}