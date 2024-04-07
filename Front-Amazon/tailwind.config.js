/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./src/**/*.{html,ts}",
  ],
  theme: {
    extend: {
      fontFamily: {
        Nunito: ['Nunito Sans', 'sans-serif'],
      },
      width: {
        'cxl': '42rem',
      },
      justify: {
        'unset': 'unset', // Custom class to unset justify-content
      },
    },
  },
  plugins: [],
}


