/**
 * table-sort.js
 * Auto-applies sortable column headers to every <table class="table"> inside .main-content.
 * Detects column data type: date (dd/mm/yyyy or yyyy-mm-dd), number, or text.
 */
(function () {
    'use strict';

    const SORT_NONE = 0, SORT_ASC = 1, SORT_DESC = 2;

    /* ── Helper: parse cell value by detected type ────────────────── */
    function detectType(cells) {
        const sample = [...cells].map(c => c.textContent.trim()).filter(v => v && v !== '—');
        if (!sample.length) return 'text';

        // Date: dd/MM/yyyy or dd/MM/yyyy HH:mm
        if (sample.every(v => /^\d{1,2}\/\d{1,2}\/\d{4}/.test(v))) return 'date';

        // ISO date
        if (sample.every(v => /^\d{4}-\d{2}-\d{2}/.test(v))) return 'isodate';

        // Number (allow spaces / dots as thousands separator)
        if (sample.every(v => /^[\d\s.,]+$/.test(v))) return 'number';

        return 'text';
    }

    function parseValue(raw, type) {
        const v = raw.trim();
        if (!v || v === '—') return type === 'number' ? -Infinity : '';
        switch (type) {
            case 'date': {
                // dd/MM/yyyy [HH:mm]
                const p = v.split(/[/\s:]/);
                return new Date(+p[2], +p[1] - 1, +p[0], +(p[3] || 0), +(p[4] || 0)).getTime();
            }
            case 'isodate':
                return new Date(v).getTime();
            case 'number':
                return parseFloat(v.replace(/[^\d.,-]/g, '').replace(',', '.')) || 0;
            default:
                return v.toLowerCase();
        }
    }

    /* ── Sort icon ────────────────────────────────────────────────── */
    function sortIcon(state) {
        if (state === SORT_ASC)  return '<i class="bi bi-sort-up sort-icon active"></i>';
        if (state === SORT_DESC) return '<i class="bi bi-sort-down sort-icon active"></i>';
        return '<i class="bi bi-arrow-down-up sort-icon"></i>';
    }

    /* ── Init one table ───────────────────────────────────────────── */
    function initTable(table) {
        if (table.dataset.sortInit) return;
        table.dataset.sortInit = '1';

        const thead = table.querySelector('thead');
        const tbody = table.querySelector('tbody');
        if (!thead || !tbody) return;

        const headers = [...thead.querySelectorAll('th')];
        const sortState = headers.map(() => SORT_NONE);   // per-column state

        headers.forEach((th, colIdx) => {
            // Skip action columns (last col or ones with buttons/links only)
            const colCells = [...tbody.querySelectorAll(`tr td:nth-child(${colIdx + 1})`)];
            const hasOnlyActions = colCells.every(td =>
                td.querySelector('a, button') && !td.textContent.trim()
            );
            if (hasOnlyActions) return;

            const type = detectType(colCells);

            // Make header clickable
            th.style.cursor = 'pointer';
            th.style.userSelect = 'none';
            th.style.whiteSpace = 'nowrap';

            // Append sort icon span
            const iconSpan = document.createElement('span');
            iconSpan.className = 'ms-1 sort-btn';
            iconSpan.innerHTML = sortIcon(SORT_NONE);
            th.appendChild(iconSpan);

            th.addEventListener('click', () => {
                // Cycle: NONE → ASC → DESC → ASC
                const prev = sortState[colIdx];
                const next = prev === SORT_ASC ? SORT_DESC : SORT_ASC;

                // Reset all other columns
                sortState.fill(SORT_NONE);
                sortState[colIdx] = next;
                headers.forEach((h, i) => {
                    const sp = h.querySelector('.sort-btn');
                    if (sp) sp.innerHTML = sortIcon(sortState[i]);
                });

                // Sort rows
                const rows = [...tbody.querySelectorAll('tr')];
                rows.sort((a, b) => {
                    const aRaw = a.querySelector(`td:nth-child(${colIdx + 1})`)?.textContent ?? '';
                    const bRaw = b.querySelector(`td:nth-child(${colIdx + 1})`)?.textContent ?? '';
                    const aVal = parseValue(aRaw, type);
                    const bVal = parseValue(bRaw, type);
                    if (aVal < bVal) return next === SORT_ASC ? -1 : 1;
                    if (aVal > bVal) return next === SORT_ASC ? 1 : -1;
                    return 0;
                });

                rows.forEach(r => tbody.appendChild(r));
            });
        });
    }

    /* ── Observe DOM for dynamically loaded tables (AJAX partials) ── */
    function scanTables() {
        document.querySelectorAll('table.table').forEach(initTable);
    }

    const observer = new MutationObserver(scanTables);

    document.addEventListener('DOMContentLoaded', () => {
        scanTables();
        observer.observe(document.body, { childList: true, subtree: true });
    });

})();
