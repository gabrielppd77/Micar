/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ['./App.tsx', './src/**/*.{js,jsx,ts,tsx}'],
  presets: [require('nativewind/preset')],
  theme: {
    extend: {
      colors: {
        brand: {
          50: '#EEF5F9',
          100: '#D7E8F0',
          200: '#B0D1E1',
          300: '#82B4CC',
          400: '#4F8FAF',
          500: '#2E6E8E',
          600: '#235777',
          700: '#1C4560',
          800: '#163449',
          900: '#0F2635',
        },
        accent: {
          50: '#FFF3E9',
          100: '#FFE1C4',
          200: '#FFC488',
          300: '#FFA34D',
          400: '#FF8A26',
          500: '#FF7A0F',
          600: '#E8630A',
          700: '#C24F08',
          800: '#9C3F09',
          900: '#7A340D',
        },
        success: {
          50: '#EAF7EF',
          100: '#CDEEDA',
          500: '#1FA35A',
          600: '#158049',
        },
        warning: {
          50: '#FFF6E6',
          100: '#FFE7B8',
          500: '#F5A623',
          600: '#D6870F',
        },
        danger: {
          50: '#FDECEC',
          100: '#F8C9C9',
          500: '#DC2626',
          600: '#B91C1C',
        },
      },
    },
  },
  plugins: [],
};
