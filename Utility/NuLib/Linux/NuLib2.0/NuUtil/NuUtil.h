
#include "NuCommon.h"

#ifndef _NUUTIL_H
#define _NUUTIL_H

#ifdef __cplusplus
extern "C" {
#endif

/* macro */
#define NU_ALIGN8(SZ) (((SZ) + 7) & ~7)
#define NU_RALIGN(SZ, N)  ( ((SZ) + ((N)-1)) & ~((N)-1) )
#define NU_LALIGN(SZ, N)  ( (SZ) & ~((N)-1) )
#define NU_RALIGN_PAGE(SZ)  ( ((SZ) + (getpagesize()-1)) & ~(getpagesize()-1) )
#define NU_LALIGN_PAGE(SZ)  ( (SZ) & ~(getpagesize()-1) )

/* single link list (LIFO)*/
typedef struct _base_single_node_t
{
    struct _base_single_node_t *next;
    struct _base_single_t      *list;
} base_single_node_t;

typedef struct _base_single_t
{
    struct _base_single_node_t *next;
    int                         cnt;
} base_single_t;

void base_single_node_init(base_single_node_t *pnode);
void base_single_init(base_single_t *plist);

int base_single_insert(base_single_t *plist, base_single_node_t  *pnode);
int base_single_remove(base_single_t *plist, base_single_node_t **pnode);

#define base_single_cnt(nusingle_list)          (nusingle_list)->cnt
#define base_single_node_next(nusingle_node)    (nusingle_node)->next
#define base_single_node_list(nusingle_node)    (nusingle_node)->list

/* double link list */
/* ----------------------------------------------------------- */
typedef struct _base_list_node_t
{
    struct _base_list_node_t *prev;
    struct _base_list_node_t *next;
    struct _base_list_t      *list;
} base_list_node_t;

typedef struct _base_list_t
{
    struct _base_list_node_t *head;
    struct _base_list_node_t *tail;
    int                       cnt;
} base_list_t;


void base_list_node_init(base_list_node_t *pnode);
void base_list_init(base_list_t *plist);

int base_list_insert_head( base_list_t *plist, base_list_node_t  *pnode );
int base_list_remove_head( base_list_t *plist, base_list_node_t **pnode );
void *base_list_remove_head2( base_list_t *plist);
int base_list_insert_tail( base_list_t *plist, base_list_node_t  *pnode );
int base_list_remove_tail( base_list_t *plist, base_list_node_t **pnode );

int base_list_remove_node( base_list_node_t *pnode );

int base_list_transfer_head(base_list_t *psrc, base_list_t *pdes);
int base_list_transfer_tail(base_list_t *psrc, base_list_t *pdes);

int base_list_items_cnt( base_list_t *plist );

#define base_list_get_head(plist)   (plist)->head
#define base_list_get_tail(plist)   (plist)->tail
#define base_list_node_next(pNode)  (pNode)->next
#define base_list_node_prev(pNode)  (pNode)->prev
#define base_list_get_list(pNode)   (pNode)->list

/* vector (LIFO) */
/* ----------------------------------------------------------- */
typedef struct _base_vector_t
{
    void    **items;
    void    **current;
    int     num;
    int     anum;
} base_vector_t;

int base_vector_new(base_vector_t **pvec, int num);
void base_vector_free(base_vector_t *pvec);
void base_vector_clear(base_vector_t *pvec);
int base_vector_push(base_vector_t *pvec, void *pdata);
int base_vector_pop(base_vector_t *pvec, void **pdata);

#define base_vector_get_by_index(nuvec_pvec, nu_idx)    ((nuvec_pvec)->items[((nu_idx) + 1)])
#define base_vector_get_cnt(nuvec_pvec)                 (nuvec_pvec)->num
#define base_vector_sort(nuvec_pvec, sort_fn)           qsort(((nuvec_pvec)->items + 1), (nuvec_pvec)->num, sizeof(void *), (sort_fn));

void *base_vector_change_by_index(base_vector_t *pvec, int idx, void *pdata);

typedef void**  base_vector_it;
#define base_vector_it_set(it, vec)     (it)=((vec)->items + 1)
#define base_vector_it_setend(it, vec)  (it)=((vec)->current - 1)
#define base_vector_it_begin(vec)       (vec)->items
#define base_vector_it_end(vec)         (vec)->current
#define base_vector_it_next(it)         ++(it)
#define base_vector_it_prev(it)         --(it)

typedef struct _base_ring_node_t
{
    struct _base_ring_node_t    *next;
} base_ring_node_t;

typedef struct _base_ring_t
{
    int                 cnt;
    base_ring_node_t    *head;
    base_ring_node_t    *tail;
} base_ring_t;

void base_ring_node_init(base_ring_node_t *pnode);
void base_ring_init(base_ring_t *pring);
int base_ring_insert_head(base_ring_t *pring, base_ring_node_t *pnode);
int base_ring_insert_tail(base_ring_t *pring, base_ring_node_t *pnode);
int base_ring_remove_head(base_ring_t *pring, base_ring_node_t **pnode);

#define base_ring_cnt(pring)        (pring)->cnt
#define base_ring_head(pring)       (pring)->head
#define base_ring_tail(pring)       (pring)->tail
#define base_ring_node_next(pnode)  (pnode)->next

#ifdef __cplusplus
}
#endif

#endif /* _NUUTIL_H */

