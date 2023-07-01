import React, { createContext, useState } from "react";

const data = [
  { Id: 1, Status: "payed", Date: "10/10/10", Amount: 22.32 },
  { Id: 2, Status: "payed", Date: "10/10/10", Amount: 22.32 },
  { Id: 3, Status: "payed", Date: "10/10/10", Amount: 22.32 },
  { Id: 4, Status: "unpayed", Date: "10/10/10", Amount: 232.32 },
  { Id: 5, Status: "payed", Date: "10/10/10", Amount: 22.32 },
];

export const InvoicesContext = createContext();

export default function Context({ children }) {
  const [invoices, setInvoices] = useState(data);
  const [open, setOpen] = useState(false);

  return (
    <InvoicesContext.Provider value={{ invoices, setInvoices, open, setOpen }}>
      {children}
    </InvoicesContext.Provider>
  );
}
