import {
  Box,
  FormControl,
  InputLabel,
  OutlinedInput,
  InputAdornment,
  Button,
  TextField,
} from "@mui/material";
import dayjs from "dayjs";
import { DatePicker } from "@mui/x-date-pickers/DatePicker";
import { LocalizationProvider } from "@mui/x-date-pickers/LocalizationProvider";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import { useContext, useState } from "react";
import { InvoicesContext } from "./Context";
import { getInvoices, apiUrl } from "./Api";
import Swal from 'sweetalert2'

export default function EditForm(props) {
  const { setInvoices, setOpen } = useContext(InvoicesContext);
  const [invoice, setInvoice] = useState({
    Id: props.invoice.Id,
    Status: props.invoice.Status,
    Date: props.invoice.Date,
    Amount: props.invoice.Amount,
  });

  const isValid = (input) => {
    if (input.Amount < 0)
    {
      setOpen(false);
      Swal.fire({
        title: 'error!',
        text: "Amount can't be negative number",
        icon: 'error',
        confirmButtonText: 'Ok'
      })
      return false;
    }
    if (input.Status === '')
    {
      setOpen(false);
      Swal.fire({
        title: 'error!',
        text: "Status can't be empty",
        icon: 'error',
        confirmButtonText: 'Ok'
      })
      return false;
    }
    return true;
  }

  const editInvoice = () => {
    if(!isValid(invoice))
      return;
    fetch(`${apiUrl}/${props.invoice.Id}`, {
      method: "PUT",
      body: JSON.stringify(invoice),
      headers: new Headers({
        "Content-Type": "application/json; charset=UTF-8",
        Accept: "application/json; charset=UTF-8",
      }),
    })
      .then((result) => {
        setOpen(false);
        Swal.fire({
          title: 'success!',
          text: 'Updated successfully!',
          icon: 'success',
          confirmButtonText: 'Ok'
        })
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
  };

  return (
    <Box>
      <FormControl sx={{ m: 1 }}>
        <TextField
          disabled
          id="outlined-disabled"
          label="Id"
          defaultValue={props.invoice.Id}
        />
        <br />
        <TextField
          required
          id="outlined-required"
          label="Status"
          defaultValue={props.invoice.Status}
          onChange={(e) =>setInvoice((prev) => ({
                  Id: prev.Id,
                  Status: e.target.value,
                  Date: prev.Date,
                  Amount: prev.Amount,
                }))
          }
        />
        <br />
        <LocalizationProvider dateAdapter={AdapterDayjs}>
          <DatePicker
            required
            label="Date picker*"
            defaultValue={dayjs(props.invoice.Date)}
            onChange={(value) =>
              setInvoice((prev) => ({
                Status: prev.Status,
                Date: dayjs(value).format("MM/DD/YYYY"),
                Amount: prev.Amount,
              }))
            }
            maxDate={dayjs(new Date())}
          />
        </LocalizationProvider>
        <br />
        <FormControl>
          <InputLabel htmlFor="outlined-adornment-amount">Amount*</InputLabel>
          <OutlinedInput
            required
            type="number"
            id="outlined-adornment-amount"
            defaultValue={props.invoice.Amount}
            endAdornment={<InputAdornment position="end">$</InputAdornment>}
            label="Amount"
            onChange={(e) =>setInvoice((prev) => ({
                    Status: prev.Status,
                    Date: prev.Date,
                    Amount: e.target.value,
                  }))
            }
          />
        </FormControl>
      </FormControl>
      <br />
      <br />
      <Button
        fullWidth
        variant="contained"
        size="medium"
        onClick={editInvoice}
        autoFocus
      >
        Save
      </Button>
    </Box>
  );
}
