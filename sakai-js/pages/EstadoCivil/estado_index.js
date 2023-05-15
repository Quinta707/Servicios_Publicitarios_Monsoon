import { Button } from 'primereact/button';
import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';
import Global from '../api/Global';
import { InputText } from 'primereact/inputtext';
import { Dialog } from 'primereact/dialog';
import React, { useState, useEffect, useRef } from 'react';
import { Toast } from 'primereact/toast';
import axios from 'axios';
import { useRouter } from 'next/router';
import { InputTextarea } from 'primereact/inputtextarea';
import { Dropdown } from 'primereact/dropdown';
import { classNames } from 'primereact/utils';

const EstadosCivilesIn = () => {

  const [posts, setPosts] = useState([]);
  const [searchText, setSearchText] = useState('');
  const [DeleteModal, setDeleteModal] = useState(false);
  const [EstadosCivilesId, setEstadosCivilesId] = useState("");
  const toast = useRef(null);
  const [EstadosCivilesDialog, setEstadosCivilesDialog] = useState(false);

  const [EstadosCivil, setEstadosCivil] = useState("");

  const [submitted, setSubmitted] = useState(false);

  const [edit, setedit] = useState([]);
  const [editDialog, seteditDialog] = useState(false);

  const [loading, setLoading] = useState(true);

  useEffect(() => {
    if (loading) {
      axios.get(Global.url + 'EstadoCivil/Listado')
        .then(response => response.data)
        .then(data => {
          setLoading(false);
          setPosts(data.data)})
        .catch(error => console.error(error))
    }
  }, [loading]);

  const openNew = () => {
    setEstadosCivilesDialog(true);
  };

  const header = (
    <div className="table-header flex flex-column md:flex-row md:justify-content-between md:align-items-center">
      <div className="grid">
        <div className="col-12">
          <Button type="button" label="Nuevo" severity="success" outlined icon="pi pi-upload" onClick={openNew} />
        </div>
      </div>
      <span className="block mt-2 md:mt-0 p-input-icon-left">
        <i className="pi pi-search" />
        <InputText type="text" value={searchText} onChange={e => setSearchText(e.target.value)} placeholder="Buscar..." />
      </span>
    </div>
  );

  const EditEstadosCivil = (eciv_Id) => {
    axios.get(Global.url + 'EstadoCivil/Buscar?id=' + eciv_Id)
      .then((r) => {
        setEstadosCivil(r.data.eciv_Descripcion)
        setEstadosCivilesId(eciv_Id)
        setedit(r.data)
      })
      .catch(error => console.error(error))

    seteditDialog(true)

  };

  const hideeditDialog = () => {
    setSubmitted(false);
    setEstadosCivil('');
    setEstadosCivilesId('');
    setedit('');
    seteditDialog(false);
  };

  const EditarP = (e) => {

    if (!EstadosCivil) {
      setSubmitted(true);
    }
    else {

      let Metodo = {
        eciv_Id: EstadosCivilesId,
        eciv_Descripcion: EstadosCivil,
        eciv_UsuModificacion: 1
      }


      axios.post(Global.url + 'EstadoCivil/Editar', Metodo)
        .then((r) => {
          setLoading(true);
          hideeditDialog();
          toast.current.show({ severity: 'success', summary: 'Accion Exitosa', detail: 'Registro Editado correctamente', life: 1500 });
        })
        .catch((e) =>{
          toast.current.show({ severity: 'warn', summary: 'Advertencia', detail: 'Ups, algo salió mal. ¡Inténtalo nuevamente!', life: 2000 });
        })
    }
  }


  const editDialogFooter = (
    <>
      <Button label="Cancelar" severity="danger" icon="pi pi-times"  onClick={hideeditDialog} />
      <Button label="Guardar" severity="success" icon="pi pi-check"  onClick={() => EditarP()} />
    </>
  );

  const OpenDeleteModal = (id) => {
    setEstadosCivilesId(id);
    setDeleteModal(true);
  };

  const hideDeleteModal = () => {
    setEstadosCivilesId("");
    setDeleteModal(false);
  };

  const EliminarEstadosCiviles = (e) => {

    let payload = {
        eciv_Id: EstadosCivilesId,
    }
    axios.post(Global.url + 'EstadoCivil/Eliminar', payload)
      .then((r) => {
        hideDeleteModal();
        setLoading(true);
        setEstadosCivilesId("");
        toast.current.show({ severity: 'success', summary: 'Accion Exitosa', detail: 'Registro Eliminado Correctamente', life: 1500 });
      })
      .catch((e) =>{
        toast.current.show({ severity: 'warn', summary: 'Advertencia', detail: 'Ups, algo salió mal. ¡Inténtalo nuevamente!', life: 2000 });
      })
  };

  const hideDialog = () => {
    setSubmitted(false)
    setEstadosCivil("");
    setEstadosCivilesDialog(false);
  };

  const EstadosCivilesDialogFooter = (
    <>
      <Button label="Cancelar" severity="danger" icon="pi pi-times"  onClick={hideDialog} />
      <Button label="Guardar" severity="success" icon="pi pi-check"  onClick={() => Agregar()} />
    </>
  );

  const Agregar = (e) => {

    if (!EstadosCivil) {
      setSubmitted(true);
    }
    else {
        var validacion = 0
        posts.forEach((posts) => {
            if (posts.eciv_Descripcion == EstadosCivil) {
                console.log(posts.eciv_Descripcion)
                console.log("entra")
                    validacion = validacion + 1;
            }
            else{
            }
          });
        if(validacion == 1){
            toast.current.show({ severity: 'warn', summary: 'Error', detail: 'El registro que intenta ingresar ya existe', life: 2000 });
        }
        else{
            let meto = {
                eciv_Descripcion: EstadosCivil,
                eciv_UsuCreacion: 1
              }
        
              axios.post(Global.url + 'EstadoCivil/Insertar', meto)
                .then((r) => {
                  hideDialog();
                  setLoading(true);
                  toast.current.show({ severity: 'success', summary: 'Accion Exitosa', detail: 'Registro Ingresado correctamente', life: 1500 });
                })
                .catch((e) =>{
                  toast.current.show({ severity: 'warn', summary: 'Advertencia', detail: 'Ups, algo salió mal. ¡Inténtalo nuevamente!', life: 2000 });
                })
        }
    }
  }

  return (
    <div className="grid">
      <div className="col-12">

        <div className="card" style={{ background: `rgb(105,101,235)`, height: '100px', width: '100%' }}>
          <div className="row text-center d-flex align-items-center">
            <h2 style={{ color: 'white' }}>Estados Civiles</h2>
          </div>
        </div>


        <div className="card">
          <Toast ref={toast} />
          <DataTable
            paginator
            className="p-datatable-gridlines "
            showGridlines
            sortField="representative.name"
            rows={10}
            rowsPerPageOptions={[5, 10, 25]}
            paginatorTemplate="FirstPageLink PrevPageLink PageLinks NextPageLink LastPageLink CurrentPageReport RowsPerPageDropdown"
            currentPageReportTemplate="Mostrando {first} a {last} de {totalRecords} registros."
            dataKey="prov_Id"
            filterDisplay="menu"
            responsiveLayout="scroll"
            emptyMessage="No se encontraron registros."
            filterMode="filter"
            value={posts}
            header={header}

          >
            <Column field="eciv_Id" header="ID" headerStyle={{ background: `rgb(105,101,235)`, color: '#fff' }} />
            <Column field="eciv_Descripcion" header="Metodo De Pago" headerStyle={{ background: `rgb(105,101,235)`, color: '#fff' }} />

            <Column
              field="acciones"
              header="Acciones"
              headerStyle={{ background: `rgb(105,101,235)`, color: '#fff' }}
              style={{ minWidth: '300px' }}
              body={rowData => (
                <div>
                  <Button label="Detalles" severity="info" icon="pi pi-eye" outlined style={{ fontSize: '0.8rem' }} />
                  <Button label="Editar" severity="warning" icon="pi pi-upload" outlined style={{ fontSize: '0.8rem' }} onClick={() => EditEstadosCivil(rowData.eciv_Id)} />
                  <Button label="Eliminar" severity="danger" icon="pi pi-trash" outlined style={{ fontSize: '0.8rem' }} onClick={() => OpenDeleteModal(rowData.eciv_Id)} />
                </div>
              )}
            />
          </DataTable>

          <Dialog visible={DeleteModal} style={{ width: '450px' }} header="Eliminar Metodo de pago" onHide={hideDeleteModal} modal footer={
            <>
              <Button label="Cancelar" icon="pi pi-times" severity="danger" onClick={hideDeleteModal} />
              <Button label="Confirmar" icon="pi pi-check" severity="success" onClick={EliminarEstadosCiviles} />
            </>
          }>
            <div className="flex align-items-center justify-content-center">
              <i className="pi pi-exclamation-triangle mr-3" style={{ fontSize: '2rem' }} />
              <span>
                ¿Desea eliminar este registro?
              </span>
            </div>
          </Dialog>

          <Dialog visible={EstadosCivilesDialog} style={{ width: '500px' }} header="Nuevo Estado Civil" modal className="p-fluid" footer={EstadosCivilesDialogFooter} onHide={hideDialog}>
            <div className="p-fluid formgrid grid">
              <div className="field col-12 ">
                <label htmlFor="EstadosCivil">Estado Civil</label>
                <InputText optionLabel="EstadosCivil" value={EstadosCivil} onChange={(e) => setEstadosCivil(e.target.value)} className={classNames({ 'p-invalid': submitted && !EstadosCivil })} />
                {submitted && !EstadosCivil && <small className="p-invalid" style={{ color: 'red' }}>El campo es requerido.</small>}
              </div>
            </div>
          </Dialog>


          <Dialog visible={editDialog} value={edit} style={{ width: '500px' }} header="Editar Estado Civil" modal className="p-fluid" footer={editDialogFooter} onHide={hideeditDialog}>
            <div className="p-fluid formgrid grid">
              <div className="field col-12">
                <label htmlFor="EstadosCivil">Estado Civil</label>
                <InputText optionLabel="EstadosCivil" value={EstadosCivil} onChange={(e) => setEstadosCivil(e.target.value)} className={classNames({ 'p-invalid': submitted && !EstadosCivil })} />
                {submitted && !EstadosCivil && <small className="p-invalid" style={{ color: 'red' }}>El campo es requerido.</small>}
              </div>
            </div>
          </Dialog>

        </div>
      </div>
    </div>
  )
}


export default EstadosCivilesIn;

