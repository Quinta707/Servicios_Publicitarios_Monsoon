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

const ProveedoresIn = () => {

  const [posts, setPosts] = useState([]);
  const [searchText, setSearchText] = useState(''); 
  const [DeleteModal, setDeleteModal] = useState(false);
  const [ProveedoresId, setProveedoresId] = useState("");
  const toast = useRef(null);
  const router = useRouter();
  const [ProveedoresDialog, setProveedoresDialog] = useState(false);

  const [Proveedor, setProveedor] = useState("");
  const [Direccion, setDireccion] = useState("");
  const [Correo, setCorreo] = useState("");

  const [DepartamentoDDL, setDepartamentoDDL] = useState([]);//ddl Departemento 
  const [Deparatemento, setDepartamento] = useState('');//almacena el valor seleccionado del ddl 

  const [MunicipioDDL, setMunicipioDDL] = useState([]);//ddl Municipios
  const [Municipio, setMunicipio] = useState('');
  const [MunicipioActivated, setMunicipioActivated] = useState(true);
  const [MunicipioSubmited, setMunicipioSubmited] = useState(false);

  const [submitted, setSubmitted] = useState(false);

  useEffect(() => {
    axios.get(Global.url + 'Proveedor/Listado')
      .then(response => response.data)
      .then(data => setPosts(data.data))
      .catch(error => console.error(error))

      axios.get(Global.url + 'Departamento/Listado')
        .then(response => response.data)
        .then((data) => setDepartamentoDDL( data.data.map((c) => ({ code: c.depa_Id, name: c.depa_Nombre }))))
        .catch(error => console.error(error))
  }, [posts]);

  const ActivarMunicipioDDl = (depa_Id) => {
    setMunicipioActivated(false);
    axios.put(Global.url + 'Municipio/MunicipioDDL?id='+ depa_Id)
        .then(response => response.data)
        .then((data) => setMunicipioDDL( data.data.map((c) => ({ code: c.muni_Id, name: c.muni_Nombre }))))
        .catch(error => console.error(error))
  };

  const openNew = () => {
    setProveedoresDialog(true);
  };

  const header = (
    <div className="table-header flex flex-column md:flex-row md:justify-content-between md:align-items-center">
      <div className="grid">
        <div className="col-12">
          <Button type="button" label="Nuevo" severity="success" outlined icon="pi pi-upload" onClick={openNew}  />
        </div>
      </div>
      <span className="block mt-2 md:mt-0 p-input-icon-left">
        <i className="pi pi-search" />
        <InputText type="text" value={searchText} onChange={e => setSearchText(e.target.value)} placeholder="Buscar..." />
      </span>
    </div>
  );


  const OpenDeleteModal = (id) => {
    console.log(id)
    setProveedoresId(id);
    setDeleteModal(true);
  };

  const hideDeleteModal = () => {
    setProveedoresId("");
    setDeleteModal(false);
  };

  const EliminarProveedores = (e) => {

    let payload = {
      prov_Id: ProveedoresId,
    }
    axios.post(Global.url + 'Proveedor/Eliminar', payload)
      .then((r) => {
        hideDeleteModal();
        setProveedoresId("");
        toast.current.show({ severity: 'success', summary: 'Accion Exitosa', detail: 'Registro Eliminado Correctamente', life: 1500 });
      });
  };

  const hideDialog = () => {
    setCorreo("");
    setProveedor("");
    setDireccion("");
    setDepartamento('');
    setMunicipio('');
    setProveedoresDialog(false);
  };

  const proveedoresDialogFooter = (
    <>
        <Button label="Cancelar" icon="pi pi-times" text onClick={hideDialog} />
        <Button label="Guardar" icon="pi pi-check" text onClick={() => Agregar()} />
    </>
  );

  const Agregar = (e) => {

    if (!Proveedor || !Correo || !Direccion ) 
    {
        setSubmitted(true);

        if(Deparatemento && !Municipio)
        {
            setMunicipioSubmited(true);
        }
    }
    else{

        let proveedor = {
            prov_Nombre:            Proveedor,
            muni_Id:                Municipio.code,
            prov_Direccion:         Direccion,
            prov_Correo:            Correo,
            prov_UsuCreacion :      1
        }

        console.log(proveedor);

        axios.post(Global.url + 'Proveedor/Insertar', proveedor)
        .then((r) => {
          hideDialog();
          toast.current.show({ severity: 'success', summary: 'Accion Exitosa', detail: 'Registro agregado correctamente', life: 1500 });
        });
    }    
}

  return (
    <div className="grid">
      <div className="col-12">

        <div className="card" style={{ background: `rgb(105,101,235)`, height: '100px', width: '100%' }}>
          <div className="row text-center d-flex align-items-center">
            <h2 style={{ color: 'white' }}>Proveedores</h2>
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
            <Column field="prov_Id" header="ID" headerStyle={{ background: `rgb(105,101,235)`, color: '#fff' }} />
            <Column field="prov_Nombre" header="Proveedor" headerStyle={{ background: `rgb(105,101,235)`, color: '#fff' }} />
            <Column field="muni_Nombre" header="Ciudad" headerStyle={{ background: `rgb(105,101,235)`, color: '#fff' }} />
            <Column field="prov_Direccion" header="Dirección" headerStyle={{ background: `rgb(105,101,235)`, color: '#fff' }} />
            <Column field="prov_Correo" header="Correo" headerStyle={{ background: `rgb(105,101,235)`, color: '#fff' }} />

            <Column
              field="acciones"
              header="Acciones"
              headerStyle={{ background: `rgb(105,101,235)`, color: '#fff' }}
              style={{ minWidth: '300px' }}
              body={rowData => (
                <div>
                  <Button label="Detalles" severity="info" icon="pi pi-eye" outlined style={{ fontSize: '0.8rem' }} /> 
                  <Button label="Editar" severity="warning" icon="pi pi-upload" outlined style={{ fontSize: '0.8rem' }} onClick={() => router.push({ pathname: './Proveedores_editar', query: { id: rowData.empe_Id } })} /> 
                  <Button label="Eliminar" severity="danger" icon="pi pi-trash" outlined style={{ fontSize: '0.8rem' }} onClick={() => OpenDeleteModal(rowData.prov_Id)} />
                </div>
              )}
            />
          </DataTable>

          <Dialog visible={DeleteModal} style={{ width: '450px' }} header="Eliminar Proveedores" onHide={hideDeleteModal} modal footer={
            <>
              <Button label="Cancelar" icon="pi pi-times" severity="danger" onClick={hideDeleteModal} />
              <Button label="Confirmar" icon="pi pi-check" severity="success" onClick={EliminarProveedores} />
            </>
          }>
            <div className="flex align-items-center justify-content-center">
              <i className="pi pi-exclamation-triangle mr-3" style={{ fontSize: '2rem' }} />
              <span>
                ¿Desea eliminar este registro?
              </span>
            </div>
          </Dialog>

          <Dialog visible={ProveedoresDialog} style={{ width: '450px' }} header="Nuevo Proveedor" modal className="p-fluid" footer={proveedoresDialogFooter} onHide={hideDialog}>             
          <div className="p-fluid formgrid grid">
              <div className="field col-12 md:col-6">
                  <label htmlFor="proveedor">Proveedor</label>
                  <InputText optionLabel="proveedor" value={Proveedor} onChange={ (e) => setProveedor(e.target.value)} className={classNames({ 'p-invalid': submitted && !Proveedor })}/>
                  {submitted && !Proveedor && <small className="p-invalid" style={{color: 'red'}}>El campo es requerido.</small>}
              </div>
              <div className="field col-12 md:col-6">
                  <label htmlFor="correo">Correo Electrónico</label>
                  <InputText type='email' optionLabel="correo" value={Correo} onChange={ (e) => setCorreo(e.target.value)} className={classNames({ 'p-invalid': submitted && !Correo })}/>
                  {submitted && !Correo && <small className="p-invalid" style={{color: 'red'}}>El campo es requerido.</small>}
              </div>
          </div>
          <div className="p-fluid formgrid grid">
              <div className="field col-12 md:col-6">
                  <label htmlFor="name">Departamento</label>
                  <Dropdown optionLabel="name" placeholder="Seleccionar" options={DepartamentoDDL} value={Deparatemento} onChange={(e) => { setDepartamento(e.value); ActivarMunicipioDDl(e.value.code); }} className={classNames({ 'p-invalid': submitted && !Deparatemento })}/>
                  {submitted && !Deparatemento && <small className="p-invalid" style={{color: 'red'}}>Seleccione una opcion.</small>}
              </div>
              <div className="field col-12 md:col-6">
                  <label htmlFor="name">Municipio</label>
                  <Dropdown optionLabel="name" placeholder="Selecionar" options={MunicipioDDL} value={Municipio} onChange={(e) => setMunicipio(e.value)} disabled={MunicipioActivated}/>
                  {MunicipioSubmited && !Municipio && <small className="p-invalid" style={{color: 'red'}}>Seleccione una opcion.</small>}
              </div>
          </div>
          <div className="p-fluid formgrid grid">
              <div className="field col-12">
                  <label optionLabel="name">Dirección</label>
                  <InputTextarea id="direccion" value={Direccion} onChange={ (e) => setDireccion(e.target.value)} className={classNames({ 'p-invalid': submitted && !Direccion })}></InputTextarea>
                  {submitted && !Direccion && <small className="p-invalid" style={{color: 'red'}}>El campo es requerido.</small>}
              </div>
          </div>
          </Dialog>

        </div>
      </div>
    </div>
  )
}


export default ProveedoresIn;

