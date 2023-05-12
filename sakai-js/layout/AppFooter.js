import React, { useContext } from 'react';
import { LayoutContext } from './context/layoutcontext';

const AppFooter = () => {
    const { layoutConfig } = useContext(LayoutContext);

    return (
        <div className="layout-footer">
            <img src={`demo/images/product/logo.jpeg`} alt="Logo" height="20" className="mr-2" />
            Por
            <span className="font-medium ml-2">Servicios Publicitarios Monsoon</span>
        </div>
    );
};

export default AppFooter;
