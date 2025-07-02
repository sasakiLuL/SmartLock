import {
  Chip,
  Paper,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TablePagination,
  TableRow,
  TableSortLabel,
} from "@mui/material";
import { TablePaginationActions } from "./TablePaginationActions";
import { Action } from "../../features/actions/Action";
import { ReactNode } from "react";
import dayjs from "dayjs";

export interface ActionsTableProps {
  actions: Action[];
  totalCount: number;
  pageSize: number;
  pageNumber: number;
  sortOrder: "asc" | "desc";
  sortColumn?: string;
  onSortColumnChange: (column: string) => void;
  onSortOrderChange: (order: "asc" | "desc") => void;
  onPageNumberChange: (
    event: React.MouseEvent<HTMLButtonElement> | null,
    newPage: number
  ) => void;
  onPageSizeChange: (
    event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
  ) => void;
}

export interface Column {
  id: "id" | "type" | "status" | "requestedOn" | "executedOn";
  label: string;
  format?: (value: any) => ReactNode;
}

export function ActionsTable({
  actions,
  totalCount,
  pageSize,
  pageNumber,
  sortOrder,
  sortColumn,
  onSortColumnChange,
  onSortOrderChange,
  onPageNumberChange,
  onPageSizeChange,
}: ActionsTableProps) {
  const handleChangePage = (
    event: React.MouseEvent<HTMLButtonElement> | null,
    newPage: number
  ) => {
    onPageNumberChange(event, newPage);
  };

  const handleChangeRowsPerPage = (
    event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
  ) => {
    onPageSizeChange(event);
  };

  const statusToChip = (state: number) => {
    switch (state) {
      case 0:
        return <Chip color="success" label="Success" />;
      case 1:
        return <Chip color="error" label="Failed" />;
      case 2:
        return <Chip color="error" label="Pending" />;
      case 3:
        return <Chip color="disabled" label="Cancelled" />;
      default:
        return <Chip color="disabled" label="Unknown" />;
    }
  };

  const typeToChip = (state: number) => {
    switch (state) {
      case 0:
        return <Chip color="disabled" label="Unlock" />;
      case 1:
        return <Chip color="disabled" label="Lock" />;
      case 2:
        return <Chip color="disabled" label="Activate" />;
      case 3:
        return <Chip color="disabled" label="Deactivate" />;
      default:
        return <Chip color="disabled" label="Unknown" />;
    }
  };

  const columns: Column[] = [
    { id: "id", label: "Id" },
    {
      id: "type",
      label: "Type",
      format: (value: number) => typeToChip(value),
    },
    {
      id: "status",
      label: "Status",
      format: (value: number) => statusToChip(value),
    },
    {
      id: "requestedOn",
      label: "Requested on",
      format: (value: Date) =>
        value ? dayjs(value).format("dddd, MMMM D, YYYY [at] h:mm A") : "",
    },
    {
      id: "executedOn",
      label: "Executed on",
      format: (value: Date) =>
        value ? dayjs(value).format("dddd, MMMM D, YYYY [at] h:mm A") : "",
    },
  ];

  return (
    <Paper sx={{ width: "100%", overflow: "hidden" }}>
      <TableContainer>
        <Table stickyHeader sx={{ minWidth: 500 }} aria-label="actions">
          <TableHead>
            <TableRow>
              {columns.map((column) => (
                <TableCell
                  key={column.id}
                  sortDirection={sortColumn === column.id ? sortOrder : false}
                >
                  <TableSortLabel
                    active={sortColumn === column.id}
                    onClick={() => {
                      onSortColumnChange(column.id);
                      onSortOrderChange(sortOrder === "asc" ? "desc" : "asc");
                    }}
                  >
                    {column.label}
                  </TableSortLabel>
                </TableCell>
              ))}
            </TableRow>
          </TableHead>
          <TableBody>
            {actions.map((row) => (
              <TableRow key={row.id}>
                {columns.map((column) => {
                  const value = row[column.id];
                  return (
                    <TableCell key={column.id}>
                      {column.format ? column.format(value) : value}
                    </TableCell>
                  );
                })}
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
      <TablePagination
        rowsPerPageOptions={[5, 10, 25, { label: "All", value: -1 }]}
        component="div"
        count={totalCount}
        rowsPerPage={pageSize}
        page={pageNumber}
        slotProps={{
          select: {
            inputProps: {
              "aria-label": "rows per page",
            },
            native: true,
          },
        }}
        onPageChange={handleChangePage}
        onRowsPerPageChange={handleChangeRowsPerPage}
        ActionsComponent={TablePaginationActions}
      />
    </Paper>
  );
}
