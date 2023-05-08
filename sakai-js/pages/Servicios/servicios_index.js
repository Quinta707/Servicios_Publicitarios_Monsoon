import React, { useState, useEffect } from 'react';
import { DataView, DataViewLayoutOptions } from 'primereact/dataview';
import { Button } from 'primereact/button';
import { MonsoonService } from '../../demo/service/MonsoonService';
import { InputText } from 'primereact/inputtext';
import { Dialog } from 'primereact/dialog';
import { classNames } from 'primereact/utils';
import { InputTextarea } from 'primereact/inputtextarea';
import { InputNumber } from 'primereact/inputnumber';
import { FileUpload } from 'primereact/fileupload';
import Modal from 'react-modal';
import axios from 'axios';

const Servicios = () => {
    let emptyProduct = {
        id: null,
        name: '',
        image: null,
        description: '',
        category: null,
        price: 0,
        quantity: 0,
        rating: 0,
        inventoryStatus: 'INSTOCK'
    };
    
    const [dataViewValue, setDataViewValue] = useState(null);
    const [globalFilterValue, setGlobalFilterValue] = useState('');
    const [filteredValue, setFilteredValue] = useState(null);
    const [layout, setLayout] = useState('grid');
    const [sortKey, setSortKey] = useState(null);
    const [sortOrder, setSortOrder] = useState(null);
    const [sortField, setSortField] = useState(null);
    const [productDialog, setProductDialog] = useState(false);
    const [product, setProduct] = useState(emptyProduct);
    const [submitted, setSubmitted] = useState(false);
    const [name, setName] = useState('');
    const [price, setPrice] = useState('');
    const [image, setImage] = useState(null);
    const [modalIsOpen, setModalIsOpen] = useState(false);
    const [dat, setDat] = useState([]);

    useEffect(() => {
        MonsoonService.getMonsoonService().then((data) => setDataViewValue(data));
        
        setGlobalFilterValue('');
    }, []);

  
    const onFilter = (e) => {
        const value = e.target.value;
        setGlobalFilterValue(value);
        if (value.length === 0) {
            setFilteredValue(null);
        } else {
            const filtered = dataViewValue.filter((product) => {
                return product.name.toLowerCase().includes(value);
            });
            setFilteredValue(filtered);
        }
    };

    const onSortChange = (event) => {
        const value = event.value;

        if (value.indexOf('!') === 0) {
            setSortOrder(-1);
            setSortField(value.substring(1, value.length));
            setSortKey(value);
        } else {
            setSortOrder(1);
            setSortField(value);
            setSortKey(value);
        }
    };
    const openNew = () => {
        setProduct(emptyProduct);
        setSubmitted(false);
        setProductDialog(true);
    };

    const dataViewHeader = (
        <div className="flex flex-column md:flex-row md:justify-content-between gap-2">
            <Button label="Nuevo" icon="pi pi-plus" severity="sucess" className="mr-2" onClick={openNew} />
            <span className="p-input-icon-left">
                <i className="pi pi-search" />
                <InputText value={globalFilterValue} onChange={onFilter} placeholder="Buscar" />
            </span>
            <DataViewLayoutOptions layout={layout} onChange={(e) => setLayout(e.value)} />
        </div>
    );

    const handleNameChange = (event) => {
        setName(event.target.value);
      };

    const handlePriceChange = (event) => {
        setPrice(event.target.value);
    };
      
    const onInputChange = (e, name) => {
        const val = (e.target && e.target.value) || '';
        let _product = { ...product };
        _product[`${name}`] = val;

        setProduct(_product);
    };

    const onInputNumberChange = (e, name) => {
        const val = e.value || 0;
        let _product = { ...product };
        _product[`${name}`] = val;

        setProduct(_product);
    };
  
    function handleImageChange(event) {
        const file = event.target.files[0];
        setImage(file);
      }

    const hideDialog = () => {
        setSubmitted(false);
        setProductDialog(false);
    };


    const productDialogFooter = (
        <>
            <Button label="Cancelar" icon="pi pi-times" text onClick={hideDialog} />
            <Button label="Guardar" icon="pi pi-check" onClick={handleSubmit} text />
        </>
    );

    const ModalInsumos = (serv_Id) => {
        setModalIsOpen(true);
        console.log(serv_Id);
        axios.put('https://localhost:44304/api/InsumosPorServicio/Listado?serv_Id='+ serv_Id)
        .then(response => response.data)
        .then((dat) => setDat(dat))
        .catch(error => console.error(error));
        <Modal isOpen={modalIsOpen} onRequestClose={ModalInsumosCerrar}>
                        <h2>Tabla de datos</h2>
                        <table>
                        <thead>
                            <tr>
                            <th>ID</th>
                            <th>Insumo</th>
                            <th>Precio</th>
                            </tr>
                        </thead>
                        <tbody>
                            {dat.map((item) => (
                            <tr key={item.insu_Id}>
                                <td>{item.insu_Id}</td>
                                <td>{item.insu_Nombre}</td>
                                <td>{item.insu_Precio}</td>
                            </tr>
                            ))}
                        </tbody>
                        </table>
                        <button onClick={ModalInsumosCerrar}>Cerrar modal</button>
                    </Modal>
        
      }

    const dataviewListItem = (data) => {
        return (
            
            <div className="col-12">
                <div className="flex flex-column md:flex-row align-items-center p-3 w-full">
                    <img src={`/demo/images/product/${data.serv_Id + '.png'}`} alt={data.serv_Id} className="my-4 md:my-0 w-9 md:w-10rem shadow-2 mr-5" />
                    <div className="flex-1 flex flex-column align-items-center text-center md:text-left">
                        <div className="font-bold text-2xl">{data.serv_Nombre}</div>
                    </div>
                    <div className="flex flex-row md:flex-column justify-content-between w-full md:w-auto align-items-center md:align-items-end mt-5 md:mt-0">
                        <span className="text-2xl font-semibold mb-2 align-self-center md:align-self-end">Lps. {data.serv_Precio}</span>
                        <Button icon="p-menuitem-icon pi pi-fw pi-list" className="mb-2 p-button-sm" onClick={ModalInsumos(data.serv_Id)}></Button>
                    </div>
                </div>
            </div>
        );
    };

      const ModalInsumosCerrar = () => {
        setModalIsOpen(false);
      }

    const dataviewGridItem = (data) => {
        return (
            <div className="col-12 lg:col-4">
                <div className="card m-3 border-1 surface-border">
                    <div className="flex flex-wrap gap-2 align-items-center justify-content-between mb-2">
                        <div className="flex align-items-center">
                            <i className="pi pi-tag mr-2" />
                            <span className="font-semibold">{data.serv_Nombre}</span>
                        </div>
                    </div>
                    <div className="flex flex-column align-items-center text-center mb-3">
                        <img src={`/demo/images/product/${data.serv_Id + '.png'}`} alt={data.serv_Nombre} className="w-9 shadow-2 my-3 mx-0" />
                        <div className="text-2xl font-bold">{data.serv_Nombre}</div>
                    </div>
                    <div className="flex align-items-center justify-content-between">
                        <span className="text-2xl font-semibold">Lps. {data.serv_Precio}</span>
                        <Button icon="p-menuitem-icon pi pi-fw pi-list" onClick={ModalInsumos(data.serv_Id)} />
                    </div>
                </div>
            </div>
        );
    };

    const itemTemplate = (data, layout) => {
        if (!data) {
            return;
        }

        if (layout === 'list') {
            return dataviewListItem(data);
        } else if (layout === 'grid') {
            return dataviewGridItem(data);
        }
    };

    async function handleSubmit(event) {
        event.preventDefault();
        
        const formData = new FormData();
        formData.append('name', name);
        formData.append('price', price);
        formData.append('image', image);
        
        try {
          const response = await fetch('/api/products', {
            method: 'POST',
            body: formData
          });
          
          const data = await response.json();
          console.log('Product created:', data);
          
          // Restablecer el formulario
          setName('');
          setPrice('');
          setImage(null);
        } catch (error) {
          console.error('Error creating product:', error);
        }
      }

    return (
        <div className="grid">
            <div className="col-12">
                <div className="card">
                    <br></br>
                    <div className="surface-0 text-700 text-center">
                        <div className="text-900 text-primary font-bold text-5xl mb-3">Nuestros servicios</div>
                        <div className="text-700 text-black text-2xl mb-5">Los mejores servicios publicitarios del país</div>
                        <br></br>
                        <DataView value={filteredValue || dataViewValue} layout={layout} paginator rows={9} sortOrder={sortOrder} sortField={sortField} itemTemplate={itemTemplate} header={dataViewHeader}></DataView>
                        <Dialog visible={productDialog} style={{ width: '450px' }} header="Nuevo Servicio" modal className="p-fluid" footer={productDialogFooter} onHide={hideDialog}>
                        {product.image && <img src={`/demo/images/product/${product.image}`} alt={product.image} width="150" className="mt-0 mx-auto mb-5 block shadow-2" />}
                        
                        <div className="p-fluid formgrid grid">
                            <div className="field col-12 md:col-6">
                                <label htmlFor="name">Nombre del servicio</label>
                                <InputText id="name" value={product.name} onChange={(e) => onInputChange(e, 'name')} required autoFocus className={classNames({ 'p-invalid': submitted && !product.name })} />
                                {submitted && !product.name && <small className="p-invalid">El nombre es requerido.</small>}
                            </div>

                            <div className="field col-12 md:col-6">
                                <label htmlFor="price" >Precio</label>
                                <InputNumber id="price" value={product.price} onValueChange={(e) => onInputNumberChange(e, 'price')} mode="currency" currency="USD" locale="en-US"  />
                            </div>

                            <div className="field col-12">
                                <label htmlFor="fail">Imagen</label>
                                <FileUpload id="fail" type="file" accept="image/*" onChange={handleImageChange} />
                            </div>  

                        </div>
                        </Dialog>
                     </div>
                </div>
                
            </div>
        </div>
        
        
    );
};

export default Servicios;
