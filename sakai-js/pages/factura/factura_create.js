import React, { useState, useEffect, useRef } from 'react';
import { Dropdown } from 'primereact/dropdown';
import { Button } from 'primereact/button';
import Global from '../api/Global';
import axios from 'axios'
import { useRouter } from 'next/router'
import { classNames } from 'primereact/utils';
import { InputNumber } from 'primereact/inputnumber';
import { Toast } from 'primereact/toast';
import { InputText } from 'primereact/inputtext';
import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';

const createFactura = () => {

    const router = useRouter();
    const toast = useRef(null); //para los toast
    const [searchText, setSearchText] = useState(''); //para la barra de busqueda

    const [Detalles, setDetalles] = useState([]); //listado de detalles

    const [submitted, setSubmitted] = useState(false);  //validar campos vacios
    const [catidadPositiva, setCantidadPositiva] = useState(false); //validar la cantidad sea positiva

    const [ClienteDDL, setClienteDDL] = useState([]); //ddl  cliente
    const [Cliente, setCliente] = useState(''); //almacena el valor seleccionado del ddl 

    const [MetosoPagoDDL, setMetosoPagoDDL] = useState([]); //ddl estados metodo pago
    const [MetodoPago, setMetodoPago] = useState(''); //almacena el valor seleccionado del ddl 

    const [ServicioDDL, setServicioDDL] = useState([]); //ddl Servicios
    const [Servicio, setServicio] = useState(''); //almacena el valor seleccionado del ddl 

    const [FacturaActivate, setFacturaActivate] = useState(false); //mantiene activados los elementos de factura
    const [FacturaDetalleActivate, setFacturaDetalleActivate] = useState(true); //mantiene activados los elementos de factura Detalle

    const [Cantidad, setCatidad] = useState(''); //cantdad de servicios a comprar
    const [FacturaId, setFacturaId] = useState(0); //capura el id de la factura ingresada

    //cargar ddl Cargos
    useEffect(() => {

        axios.get(Global.url + 'Cliente/Listado')
            .then(response => response.data)
            .then((data) => setClienteDDL(data.data.map((c) => ({ code: c.clie_Id, name: `${c.clie_Nombres} ${c.clie_Apellidos}` }))))
            .catch(error => console.error(error))

        axios.get(Global.url + 'MetododePago/Listado')
            .then(response => response.data)
            .then((data) => setMetosoPagoDDL(data.data.map((c) => ({ code: c.meto_Id, name: c.meto_Descripcion }))))
            .catch(error => console.error(error))

        axios.get(Global.url + 'Servicio/Listado')
            .then(response => response.data)
            .then((data) => setServicioDDL(data.data.map((c) => ({ code: c.serv_Id, name: c.serv_Nombre }))))
            .catch(error => console.error(error))
    }, []);

    

    //listado de detalles
    useEffect(() => {
        axios.put(Global.url + 'Factura/ListadoDetalles?id=' + FacturaId)
            .then(response => response.data)
            .then(data => setDetalles(data.data))
            .catch(error => console.error(error))
    }, [Detalles]);
    
   


    //enviar Factura
    const EnviarFactura = (e) => {

        if (!Cliente || !MetodoPago) {
            setSubmitted(true)
        }
        else {

            let Fcatura = {
                clie_Id: Cliente.code,
                empe_Id: 1,
                meto_Id: MetodoPago.code,
                fact_UsuCreacion: 1
            }


            axios.post(Global.url + 'Factura/Insertar', Fcatura)
                .then((r) => {
                    setFacturaId(r.data.data.codeStatus);
                })
            setFacturaDetalleActivate(false);
            setFacturaActivate(true);
        }
    }


    //enviar Factura Detalle
    const EnviarFacturaDetalle = (e) => {

        if (!Servicio || !Cantidad) {
            setSubmitted(true)
        }
        else {

            if (Cantidad < 1) {
                setCantidadPositiva(true);
            }
            else {
                setCantidadPositiva(false);
                let FcaturaDetalle = {
                    fact_Id: FacturaId,
                    serv_Id: Servicio.code,
                    fdet_Cantidad: Cantidad,
                    fdet_UsuCreacion: 1
                }
                axios.post(Global.url + 'Factura/InsertarDetalles', FcaturaDetalle)
                    .then((r) => {
                    })
            }
        }
    }

    const header = (
        <div className="table-header flex flex-column md:flex-row md:justify-content-between md:align-items-center">
            <div className="grid">
                <div className="col-12">

                </div>
            </div>
            <span className="block mt-2 md:mt-0 p-input-icon-left">
                <i className="pi pi-search" />
                <InputText type="text" value={searchText} onChange={e => setSearchText(e.target.value)} placeholder="Buscar..." />
            </span>
        </div>
    );

    return (
        <div className='col-12'>
            <div className="card" style={{ background: `rgb(105,101,235)`, height: '100px', width: '100%' }}>
                <div className="row text-center d-flex align-items-center">
                    <h2 style={{ color: 'white' }}>Crear Factura</h2>
                </div>
            </div>

            <div className="grid p-fluid">
                <div className='col-5'>
                    <div className='card'>
                        <div className="grid p-fluid">

                            <div className='col-12'>
                                <div className="field">
                                    <label htmlFor="Cliente">Cliente</label><br />
                                    <Dropdown optionLabel="name" placeholder="Selecionar" options={ClienteDDL} value={Cliente} onChange={(e) => setCliente(e.value)} className={classNames({ 'p-invalid': submitted && !Cliente })} disabled={FacturaActivate} />
                                    {submitted && !Cliente && <small className="p-invalid" style={{ color: 'red' }}>Seleccione una opcion.</small>}
                                </div>
                            </div>

                            <div className='col-12'>
                                <div className="field mt-1">
                                    <label htmlFor="MetdoPago">Metodo de Pago</label><br />
                                    <Dropdown optionLabel="name" placeholder="Selecionar" options={MetosoPagoDDL} value={MetodoPago} onChange={(e) => setMetodoPago(e.value)} className={classNames({ 'p-invalid': submitted && !MetodoPago })} disabled={FacturaActivate} />
                                    {submitted && !MetodoPago && <small className="p-invalid" style={{ color: 'red' }}>Seleccione una opcion.</small>}
                                </div>
                            </div>

                            <div className='col-12'>
                                <div className="grid p-fluid">
                                    <div className='col-5'>
                                        <Button label="Crear" severity="success" onClick={() => EnviarFactura()} disabled={FacturaActivate} />
                                    </div>
                                    <div className='col-5'>
                                        <Button label="Cancelar" severity="danger" onClick={() => router.push('./factura_index')} />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div className='col-7'>
                    <div className='card'>
                        <div className="grid p-fluid">

                            <div className='col-12'>
                                <div className="field">
                                    <label htmlFor="Servicio">Servicio</label><br />
                                    <Dropdown optionLabel="name" placeholder="Selecionar" options={ServicioDDL} value={Servicio} onChange={(e) => setServicio(e.value)} className={classNames({ 'p-invalid': submitted && !Servicio })} disabled={FacturaDetalleActivate} />
                                    {submitted && !Servicio && <small className="p-invalid" style={{ color: 'red' }}>Seleccione una opcion.</small>}
                                </div>
                            </div>

                            <div className='col-12'>
                                <div className="field">
                                    <label htmlFor="Servicio">Cantidad</label><br />
                                    <InputNumber type='Text' value={Cantidad} onValueChange={(e) => setCatidad(e.value)} showButtons mode="decimal" className={classNames({ 'p-invalid': submitted && !Cantidad })} disabled={FacturaDetalleActivate} />
                                    {submitted && !Cantidad && <small className="p-invalid" style={{ color: 'red' }}>Campo Obligatorio.</small>}
                                    {catidadPositiva && <small className="p-invalid" style={{ color: 'red' }}>Ingrese una cantidad positiva.</small>}
                                </div>

                            </div>

                            <div className='col-12'>
                                <div className="grid p-fluid">
                                    <div className='col-4'>
                                        <Button label="Agregar" severity="success" onClick={() => EnviarFacturaDetalle()} disabled={FacturaDetalleActivate} />
                                    </div>

                                </div>
                            </div>

                        </div>
                    </div>
                </div>

                <div className='col-12'>
                    <div className='card'>
                        <div className="grid p-fluid">
                            <div className='col-12'>
                                <Toast ref={toast} />
                                <DataTable
                                    paginator
                                    className="p-datatable-gridlines"
                                    showGridlines
                                    sortField="representative.name"
                                    rows={10}
                                    rowsPerPageOptions={[5, 10, 25]}
                                    paginatorTemplate="FirstPageLink PrevPageLink PageLinks NextPageLink LastPageLink CurrentPageReport RowsPerPageDropdown"
                                    currentPageReportTemplate="Mostrando {first} a {last} de {totalRecords} registros."
                                    dataKey="cate_Id"
                                    filterDisplay="menu"
                                    responsiveLayout="scroll"
                                    emptyMessage="No se encontrron registros."
                                    filterMode="filter"
                                    header={header}
                                    value={Detalles.filter((post) =>
                                        post.serv_Nombre.toLowerCase().includes(searchText.toLowerCase()) ||
                                        post.serv_Precio.toLowerCase().includes(searchText.toLowerCase()) ||
                                        post.fdet_Cantidad.toLowerCase().includes(searchText.toLowerCase())
                                    )}

                                >
                                    <Column field="serv_Nombre" header="servicio" headerStyle={{ background: `rgb(105,101,235)`, color: '#fff' }} />
                                    <Column field="serv_Precio" header="Precio" headerStyle={{ background: `rgb(105,101,235)`, color: '#fff' }} />
                                    <Column field="fdet_Cantidad" header="Cantidad" headerStyle={{ background: `rgb(105,101,235)`, color: '#fff' }} />

                                    <Column
                                        field="acciones"
                                        header="Acciones"
                                        headerStyle={{ background: `rgb(105,101,235)`, color: '#fff' }}
                                        style={{ minWidth: '300px' }}
                                        body={rowData => (
                                            <div>
                                                <Button label="Eliminar" severity="danger" icon="pi pi-trash" outlined style={{ fontSize: '0.8rem' }} />
                                            </div>
                                        )}
                                    />
                                </DataTable>
                            </div>
                        </div>
                    </div>

                </div>

            </div>
        </div>

    )


}

export default createFactura;