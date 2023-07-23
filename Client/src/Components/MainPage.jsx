import Invoice from './Invoice';
import InsertForm from './InsertForm';
import { useContext, useEffect } from 'react';
import { getInvoices } from './Api';
import { InvoicesContext } from "./Context";

function MainPage() {
  const {
    setInvoices,invoices
  } = useContext(InvoicesContext);

  useEffect(() => {
    getInvoices()
    .then((result) => {
      setInvoices(result);
    })
    .catch((error) => {
      console.error(error);
    });
  }, []);
  
  return (
    <div>
      <h1 style={{textAlign:"center", fontSize:50}}>Welcome to your invoices data base</h1>
      <h2 style={{textAlign:"center"}}>Here you can insert a new invoice</h2>
      <InsertForm/>
      <br/><hr/>
      <h1 style={{textAlign:"center"}}>Your invoices</h1>
      <div style={{display:'flex',flexWrap:'wrap',justifyContent:"space-around"}}>
        {invoices.map((invoice)=> <Invoice key={invoice.Id} invoice={invoice}/>)}
      </div>
    </div>
  );
}

export default MainPage;
