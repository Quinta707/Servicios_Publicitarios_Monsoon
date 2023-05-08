import { useRouter } from 'next/router'
import React, { useState, useEffect, useRef } from 'react';
import axios from 'axios'
import Global from '../api/Global';
import { Fieldset } from 'primereact/fieldset';
import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';

const Detalle = () => {

    const router = useRouter();
    const [FacturaId, setFacturaId] = useState(router.query.id); // optiene el id del resgitro a editar

    const [FacturaDatos, setFacturaDatos] = useState([]);
    const [cliente, setcliente] = useState('');
    const [Empleado, setEmpleado] = useState('');
    const [sucursal, setSucursal] = useState('');
    const [metodopago, setmetodopago] = useState('');
    const [FechaCompra, setFechaCompra] = useState('');
    const [SubTotal, setSubTotal] = useState('');
    const [IVA, setIVA] = useState('');
    const [Total, setTotal] = useState('');

    const [UsuarioCreacion, setUsuarioCreacion] = useState('');
    const [FechaCreacion, setFechaCreacion] = useState('')

    useEffect(() => {
        console.log(FacturaId);
        axios.get(Global.url + 'Factura/Buscar?id=' + FacturaId)
            .then((r) => {
                setcliente(r.data.clie_NombreCompleto)
                setEmpleado(r.data.empe_NombreCompleto)
                setSucursal(r.data.sucu_Nombre)
                setmetodopago(r.data.meto_Descripcion)
                setFechaCompra(r.data.fact_FechaCompra)

                setUsuarioCreacion(r.data.user_Creacion)
                setFechaCreacion(r.data.user_Modificacion)
            })

        axios.put(Global.url + 'Factura/ListadoDetalles?id=' + FacturaId)
            .then((r) => {
                setFacturaDatos(r.data.data);
            })

        axios.put(Global.url + 'Factura/PrecioDetalles?id=' + FacturaId)
            .then((r) => {
                setSubTotal(r.data[0].SubTotal);
                setIVA(r.data[0].IVA);
                setTotal(r.data[0].Total);
            })

    }, []);

    return (

        <div className="grid">
            <div className="col-12">

                <div className="card" style={{ background: `rgb(105,101,235)`, height: '100px', width: '100%' }}>
                    <div className="row text-center d-flex align-items-center">
                        <h2 style={{ color: 'white' }}>Detalles</h2>
                    </div>
                </div>

                <div className="card">
                    <div className="grid p-fluid">
                        <div className='col-6'>
                            <h5>Factura Id: </h5> <label>{FacturaId}</label>
                        </div>
                        <div className='col-6'>

                        </div>

                        <div className='col-6 mt-2'>
                            <h5>Cliente: </h5>
                            <label>{cliente}</label>
                        </div>
                        <div className='col-6 mt-2'>
                            <h5>Empleado: </h5> <label>{Empleado}</label>
                        </div>

                        <div className='col-6 mt-2'>
                            <h5>Sucursal: </h5> <label>{sucursal}</label>
                        </div>
                        <div className='col-6 mt-2'>
                            <h5>Metdodo de pago: </h5> <label>{metodopago}</label>
                        </div>

                        <div className='col-6 mt-2'>
                            <h5>FechaCompra: </h5> <label>{FechaCompra}</label>
                        </div>

                        <div className='col-12 mt-2'>


                            <Fieldset legend="Auditoria" toggleable>

                                <div className="grid p-fluid">
                                    <div className='col-4 mt-2'>
                                        <h5>Servicio</h5>
                                        {FacturaDatos.map((item, index) => (
                                            <div key={index}>
                                                {/* Aquí puedes mostrar los elementos del arreglo */}
                                                <label>- {item.serv_Nombre}</label>
                                            </div>
                                        ))}

                                    </div>
                                    <div className='col-2 mt-2'>
                                        <h5>Precio</h5>
                                        {FacturaDatos.map((item, index) => (
                                            <div key={index}>
                                                {/* Aquí puedes mostrar los elementos del arreglo */}
                                                <label> {item.serv_Precio} .Lps</label>
                                            </div>
                                        ))}

                                    </div>
                                    <div className='col-2 mt-2'>
                                        <h5>Cantidad</h5>
                                        {FacturaDatos.map((item, index) => (
                                            <div key={index}>
                                                {/* Aquí puedes mostrar los elementos del arreglo */}
                                                <label> {item.fdet_Cantidad}</label>
                                            </div>
                                        ))}

                                    </div>
                                    <div className='col-4 mt-2'>
                                        <h5>Total a Pagar</h5>
                                        <div className="grid p-fluid">
                                            <div className='col-4'>
                                                <label>SubTotal:    </label><br />
                                                <label>IVA:         </label><br />
                                                <label>Total:       </label><br />
                                            </div>
                                            <div className='col-4'>
                                                <label>{SubTotal} .Lps</label><br />
                                                <label>{IVA} .Lps</label><br />
                                                <label>{Total} .Lps</label><br />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </Fieldset>


                        </div>

                        <div className='col-12'>

                            <Fieldset legend="Servicios Comprados" toggleable>
                                <div className="grid p-fluid">
                                    <div className='col-12 mt-2'>
                                        <DataTable

                                            className="p-datatable-gridlines"
                                            showGridlines
                                            sortField="representative.name"
                                            currentPageReportTemplate="Mostrando {first} a {last} de {totalRecords} registros."
                                            dataKey="cate_Id"
                                            filterDisplay="menu"
                                            responsiveLayout="scroll"
                                            emptyMessage="No se encontrron registros."

                                        >
                                            <Column header="Usuario Creacion"       headerStyle={{ background: `rgb(105,101,235)`, color: '#fff' }} body={UsuarioCreacion} />
                                            <Column header="Fecha Creacion"         headerStyle={{ background: `rgb(105,101,235)`, color: '#fff' }} />
                                            <Column header="Usuario Modificacion"   headerStyle={{ background: `rgb(105,101,235)`, color: '#fff' }} />
                                            <Column header="Fecha Modificacion"     headerStyle={{ background: `rgb(105,101,235)`, color: '#fff' }} />

                                        </DataTable>
                                    </div>
                                </div>
                            </Fieldset>
                        </div>

                    </div>

                </div>

            </div>
        </div>
    )

}

export default Detalle;