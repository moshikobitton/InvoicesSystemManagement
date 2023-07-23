import { Box, Typography, Button, Dialog, DialogTitle } from "@mui/material";
import InvoicePic from "../InvoicePic.png";
import { useContext } from "react";
import EditForm from "./EditForm";
import { getInvoices, apiUrl } from "./Api";
import { InvoicesContext } from "./Context";
import Swal from "sweetalert2";

export default function Invoice(props) {
  const { setInvoices, open, setOpen, invoice, setInvoice } = useContext(InvoicesContext);

  const handleClickOpen = () => {
    setOpen(true);
    setInvoice(props.invoice);
  };
  const handleClose = () => {
    setOpen(false);
  };

  const deleteInvoice = () => {
    fetch(`${apiUrl}/${props.invoice.Id}`, {
      method: "DELETE",
      headers: new Headers({
        "Content-Type": "application/json; charset=UTF-8",
        Accept: "application/json; charset=UTF-8",
      }),
    })
      .then((result) => {
        Swal.fire({
          title: "success!",
          text: "Deleted successfully!",
          icon: "success",
          confirmButtonText: "Ok",
        });
        getInvoices()
          .then((result) => {
            setInvoices(result);
            getInvoices()
              .then((result) => {
                setInvoices(result);
              })
              .catch((error) => {
                console.error(error);
              });
          })
          .catch((error) => {
            console.error(error);
          });
      })
      .catch((error) => {
        console.log(error);
      });
  };

  return (
    <Box
      style={{
        display: "flex",
        flexDirection: "column",
        width: "fit-content",
        borderRadius: 20,
        padding: 20,
        backgroundColor: "white",
        margin: 10,
      }}
    >
      <Box style={{ borderBottom: "1px solid" }}>
        <img src={InvoicePic} style={{ width: 200, borderRadius: 20 }} />
      </Box>
      <Typography style={{ textAlign: "center" }}>
        <br />
        Status : {props.invoice.Status}
        <br />
        <br />
        Date : {props.invoice.Date}
        <br />
        <br />
        Amount : {props.invoice.Amount} $
        <br />
        <br />
      </Typography>
      <div>
        <Box style={{ display: "flex", justifyContent: "space-evenly" }}>
          <Button variant="contained" size="medium" onClick={handleClickOpen}>
            Edit/View
          </Button>
          <Button variant="contained" size="medium" onClick={deleteInvoice}>
            Delete
          </Button>
        </Box>
        <Dialog
          open={open}
          onClose={handleClose}
          aria-labelledby="alert-dialog-title"
          aria-describedby="alert-dialog-description"
        >
          <Box style={{ padding: 20 }}>
            <DialogTitle id="alert-dialog-title">{"Edit Invoice"}</DialogTitle>
            <EditForm invoice={invoice} />
            <br />
            <Button
              fullWidth
              variant="contained"
              size="medium"
              onClick={handleClose}
            >
              Close
            </Button>
          </Box>
        </Dialog>
      </div>
    </Box>
  );
}
