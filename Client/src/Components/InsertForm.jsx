import {
  Box,
  FormControl,
  InputLabel,
  OutlinedInput,
  InputAdornment,
  TextField,
  Button
} from "@mui/material";
import InvoicePic from "../InvoicePic.png";
import dayjs from "dayjs";
import { DatePicker } from "@mui/x-date-pickers/DatePicker";
import { LocalizationProvider } from "@mui/x-date-pickers/LocalizationProvider";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import { useContext, useState } from 'react';
import { InvoicesContext } from "./Context";
import { getInvoices, apiUrl } from "./Api";
import Swal from 'sweetalert2'

export default function InsertForm(props) {
  const {setInvoices} = useContext(InvoicesContext);
  const emptyInvoice = {Date:dayjs(new Date()).format("MM/DD/YYYY"),Status:"", Amount:0}
  const [invoice, setInvoice] = useState(emptyInvoice);

  const isValid = (input) => {
    if (input.Amount < 0)
    {
      Swal.fire({
        title: 'Error!',
        text: "Amount can't be negative number",
        icon: 'error',
        confirmButtonText: 'Ok'
      })
      return false;
    }
    if (input.Status === '')
    {
      Swal.fire({
        title: 'Error!',
        text: "Status can't be empty",
        icon: 'error',
        confirmButtonText: 'Ok'
      })
      return false;
    }
    return true;
  }

  const insertInvoice = async () => {
    if(!isValid(invoice))
      return;
    fetch(apiUrl, {
      method: "POST",
      body: JSON.stringify(invoice),
      headers: new Headers({
        "Content-Type": "application/json; charset=UTF-8",
        //"Accept": "application/json; charset=UTF-8",
      }),
    })
      .then((result) => {
        Swal.fire({
          title: 'Success!',
          text: 'Added successfully!',
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

  const clear = () => {
    setInvoice(emptyInvoice);
  };
  
  return (
    <Box style={{display:"flex", flexDirection:"row", justifyContent:"center"}}>
      <Box style={{display:"flex",borderRadius:10, padding:20, flexDirection:"row", justifyContent:"center",width:"fitContent", backgroundColor:"white"}} >
      <Box style={{display:"flex", marginLeft:"10%",marginRight:"7%"}}> <img src={InvoicePic} style={{ width: 200, borderRadius: 20 }} /></Box>
      <Box style={{display:"flex", width:500}}>
        <FormControl fullWidth sx={{ m: 1 }}>
          <TextField
          required
          id="outlined-required"
          label="Status"
          value={invoice.Status}
          onChange={(e)=>setInvoice((prev)=>({ Date:prev.Date, Status:e.target.value, Amount:parseInt(prev.Amount)}))}
        />
        <br />
          <LocalizationProvider dateAdapter={AdapterDayjs}>
            <DatePicker
              required
              label="Date picker*"
              value={dayjs(invoice.Date)}
              maxDate={dayjs(new Date())}
              onChange={(value)=>setInvoice((prev)=>({ Date:dayjs(value).format("MM/DD/YYYY"), Status:prev.Status, Amount:parseInt(prev.Amount)}))}
            />
          </LocalizationProvider>
          <br />
          <FormControl>
            <InputLabel htmlFor="outlined-adornment-amount">Amount*</InputLabel>
            <OutlinedInput
              required
              type="number"
              id="outlined-adornment-amount"
              endAdornment={<InputAdornment position="end">$</InputAdornment>}
              label="Amount"
              value={invoice.Amount}
              onChange={(e)=>setInvoice((prev)=>({ Date:prev.Date, Status:prev.Status, Amount:parseInt(e.target.value)}))}
            />
          </FormControl>
          <br />
          
          <Box style={{ display: "flex", justifyContent:"space-between" }}>
          <Button style={{ margin:1 }} fullWidth variant="contained" size="medium" onClick={insertInvoice}>
            Add
          </Button>
          <Button style={{ margin:1 }} fullWidth variant="contained" size="medium" onClick={clear}>
            Clear
          </Button>
        </Box>
        </FormControl>
        <br />
      </Box>
      </Box>
    </Box>
  )
}
