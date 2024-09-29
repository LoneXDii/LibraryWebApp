import { Button, Modal } from "react-bootstrap";
import { BookService } from "../../../../Services/BookService";


export default function DeleteBookModal({id, show, handleClose}){

    function handleDelete(){
        BookService.deleteBook(id)
        handleClose()
    }

    return(
        <Modal show={show} onHide={handleClose}>
            <Modal.Header closeButton>
                <Modal.Title>Delete book</Modal.Title>
            </Modal.Header>
            <Modal.Body>Are you sure you want to delete this book?</Modal.Body>
            <Modal.Footer>
                <Button variant="secondary" onClick={handleClose}>
                    Close
                </Button>
                <Button variant="primary" onClick={handleDelete}>
                    Delete
                </Button>
            </Modal.Footer>
        </Modal>
    )
}